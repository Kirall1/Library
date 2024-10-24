using Library.BusinessAccess.Models.Book;

namespace Library.BusinessAccess.UseCases.Books;

public interface IAddBookUseCase
{
    Task<BookCreateResponseDto> ExecuteAsync(BookCreateDto bookToCreate, CancellationToken cancellationToken);
}