using Library.BusinessAccess.Exceptions;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Book;
using Library.Shared;

namespace Library.BusinessAccess.UseCases.Books.Impl;

public class TakeBookUseCase : ITakeBookUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public TakeBookUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponseDto> ExecuteAsync(BookTakeReturnDto bookToTake, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(bookToTake.Id, cancellationToken);
        if (book == null)
            throw new NotFoundException("Book not found");
        book.TakeBook(bookToTake.UserId);
        _unitOfWork.Books.Update(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new BaseResponseDto() { Id = book.Id };
    }
}
