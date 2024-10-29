using AutoMapper;
using Library.BusinessAccess.Exceptions;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Book;

namespace Library.BusinessAccess.UseCases.Books.Impl;

public class GetBookByIdUseCase : IGetBookByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBookByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookDetailedResponseDto> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(id, cancellationToken);
        if (book == null)
            throw new NotFoundException("Book not found");

        return _mapper.Map<BookDetailedResponseDto>(book);
    }
}
