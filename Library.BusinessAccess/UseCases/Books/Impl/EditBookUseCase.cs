using AutoMapper;
using Library.BusinessAccess.Exceptions;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Book;
using Library.BusinessAccess.Services;

namespace Library.BusinessAccess.UseCases.Books.Impl;

public class EditBookUseCase : IEditBookUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public EditBookUseCase(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
    }

    public async Task<BaseResponseDto> ExecuteAsync(BookUpdateDto bookToUpdate, CancellationToken cancellationToken)
    {
        if ((await _unitOfWork.Books.GetByIsbnAsync(bookToUpdate.Isbn, cancellationToken)) != null)
        {
            throw new ConflictException("Book with this ISBN already exists");
        }
        
        var book = await _unitOfWork.Books.GetByIdAsync(bookToUpdate.Id, cancellationToken);
        if (book == null)
            throw new NotFoundException("Book not found");
        
        _mapper.Map(bookToUpdate, book);

        if (bookToUpdate.ImageFile != null)
        {
            var imagePath = await _fileService.SaveFileAsync(bookToUpdate.ImageFile, ["png", "jpg", "jpeg"], cancellationToken);
            if (book.Image != null)
                _fileService.DeleteFile(book.Image);
            book.Image = imagePath;
        }

        _unitOfWork.Books.Update(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new BaseResponseDto() { Id = book.Id };
    }
}
