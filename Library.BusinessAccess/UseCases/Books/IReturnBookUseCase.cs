using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Book;

namespace Library.BusinessAccess.UseCases.Books;

public interface IReturnBookUseCase
{
    Task<BaseResponseDto> ExecuteAsync(BookTakeReturnDto bookToReturn, CancellationToken cancellationToken);
}