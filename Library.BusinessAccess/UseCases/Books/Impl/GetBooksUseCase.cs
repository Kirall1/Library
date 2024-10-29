using AutoMapper;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Book;

namespace Library.BusinessAccess.UseCases.Books.Impl;

public class GetBooksUseCase : IGetBooksUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBooksUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookBaseResponseDto>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.Books.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<BookBaseResponseDto>>(books);
    }
}
