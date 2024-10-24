using Library.BusinessAccess.Models;

namespace Library.BusinessAccess.UseCases.Authors;

public interface IDeleteAuthorUseCase
{
    Task<BaseResponseDto> ExecuteAsync(int id, CancellationToken cancellationToken);
}