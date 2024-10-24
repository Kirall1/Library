using Library.BusinessAccess.Models.Book;

namespace Library.BusinessAccess.UseCases.Books;

public interface IGetBooksByPageUseCase
{
    Task<IEnumerable<BookBaseResponseDto>> ExecuteAsync(int page, int pageSize, CancellationToken cancellationToken);
}