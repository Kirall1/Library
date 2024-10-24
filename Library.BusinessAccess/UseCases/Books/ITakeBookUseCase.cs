using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Book;

namespace Library.BusinessAccess.UseCases.Books;

public interface ITakeBookUseCase
{
    Task<BaseResponseDto> ExecuteAsync(BookTakeReturnDto bookToTake, CancellationToken cancellationToken);
}
