using AutoMapper;
using Library.BusinessAccess.Exceptions;
using Library.BusinessAccess.Models.Book;
using Library.BusinessAccess.Services;
using Library.Shared;
using Library.Domain;

namespace Library.BusinessAccess.UseCases.Books.Impl;

public class AddBookUseCase : IAddBookUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public AddBookUseCase(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }

    public async Task<BookCreateResponseDto> ExecuteAsync(BookCreateDto bookToCreate, CancellationToken cancellationToken)
    {
        if ((await _unitOfWork.Books.GetByIsbnAsync(bookToCreate.Isbn, cancellationToken)) != null)
        {
            throw new ConflictException("Book with this ISBN already exists");
        }

        var imagePath = string.Empty;
        if (bookToCreate.ImageFile != null)
            imagePath = await _fileService.SaveFileAsync(bookToCreate.ImageFile, ["png", "jpg", "jpeg"], cancellationToken);

        var book = _mapper.Map<Book>(bookToCreate);
        if (imagePath != string.Empty)
            book.Image = imagePath;

        book = await _unitOfWork.Books.AddAsync(book, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<BookCreateResponseDto>(book);
    }
}
