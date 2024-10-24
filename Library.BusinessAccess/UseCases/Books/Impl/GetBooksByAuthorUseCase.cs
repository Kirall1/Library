using AutoMapper;
using Library.BusinessAccess.Models.Book;
using Library.Shared;

namespace Library.BusinessAccess.UseCases.Books.Impl;

public class GetBooksByAuthorUseCase : IGetBooksByAuthorUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBooksByAuthorUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookBaseResponseDto>> ExecuteAsync(int authorId, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(authorId, cancellationToken);
        var books = author.Books;
        return _mapper.Map<IEnumerable<BookBaseResponseDto>>(books);
    }
}
