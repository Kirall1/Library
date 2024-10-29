using AutoMapper;
using Library.BusinessAccess.Exceptions;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Book;

namespace Library.BusinessAccess.UseCases.Books.Impl;

public class GetBookByIsbnUseCase : IGetBookByIsbnUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBookByIsbnUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookBaseResponseDto> ExecuteAsync(string isbn, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIsbnAsync(isbn, cancellationToken);
        if (book == null)
            throw new NotFoundException("Book not found");

        return _mapper.Map<BookBaseResponseDto>(book);
    }
}