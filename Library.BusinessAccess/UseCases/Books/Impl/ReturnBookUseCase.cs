using Library.BusinessAccess.Exceptions;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Book;
using Library.Shared;

namespace Library.BusinessAccess.UseCases.Books.Impl;

public class ReturnBookUseCase : IReturnBookUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public ReturnBookUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponseDto> ExecuteAsync(BookTakeReturnDto bookToReturn, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(bookToReturn.Id, cancellationToken);
        if (book == null)
            throw new NotFoundException("Book not found");
        book.ReturnBook();
        _unitOfWork.Books.Update(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new BaseResponseDto() { Id = book.Id };
    }
}
