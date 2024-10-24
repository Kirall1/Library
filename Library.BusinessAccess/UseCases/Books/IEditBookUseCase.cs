using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Book;

namespace Library.BusinessAccess.UseCases.Books;

public interface IEditBookUseCase
{
    Task<BaseResponseDto> ExecuteAsync(BookUpdateDto bookToUpdate, CancellationToken cancellationToken);
}