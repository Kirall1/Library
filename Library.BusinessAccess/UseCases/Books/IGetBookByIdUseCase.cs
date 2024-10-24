using Library.BusinessAccess.Models.Book;

namespace Library.BusinessAccess.UseCases.Books;

public interface IGetBookByIdUseCase
{
    Task<BookDetailedResponseDto> ExecuteAsync(int id, CancellationToken cancellationToken);
}