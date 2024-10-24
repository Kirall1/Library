using Library.BusinessAccess.Models.Book;

namespace Library.BusinessAccess.UseCases.Books;

public interface IGetBooksByAuthorUseCase
{
    Task<IEnumerable<BookBaseResponseDto>> ExecuteAsync(int authorId, CancellationToken cancellationToken);
}