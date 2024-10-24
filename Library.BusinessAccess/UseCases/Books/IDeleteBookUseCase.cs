using Library.BusinessAccess.Models;

namespace Library.BusinessAccess.UseCases.Books;

public interface IDeleteBookUseCase
{
    Task<BaseResponseDto> ExecuteAsync(int id, CancellationToken cancellationToken);
}