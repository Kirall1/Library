using Library.BusinessAccess.Models.Book;

namespace Library.BusinessAccess.UseCases.Books;

public interface IGetBooksUseCase
{
    Task<IEnumerable<BookBaseResponseDto>> ExecuteAsync(CancellationToken cancellationToken);
}