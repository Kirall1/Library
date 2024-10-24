using Library.BusinessAccess.Models.Author;

namespace Library.BusinessAccess.UseCases.Authors;

public interface IGetAuthorByIdUseCase
{
    Task<AuthorResponseDto> ExecuteAsync(int id, CancellationToken cancellationToken);
}