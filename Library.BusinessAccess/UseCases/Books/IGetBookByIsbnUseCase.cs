using Library.BusinessAccess.Models.Book;

namespace Library.BusinessAccess.UseCases.Books;

public interface IGetBookByIsbnUseCase
{
    Task<BookBaseResponseDto> ExecuteAsync(string isbn, CancellationToken cancellationToken);
}