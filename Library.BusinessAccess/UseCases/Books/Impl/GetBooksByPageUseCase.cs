using AutoMapper;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Book;

namespace Library.BusinessAccess.UseCases.Books.Impl;

public class GetBooksByPageUseCase : IGetBooksByPageUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBooksByPageUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookBaseResponseDto>> ExecuteAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.Books.GetByPageAsync(page, pageSize, cancellationToken);
        return _mapper.Map<IEnumerable<BookBaseResponseDto>>(books);
    }
}