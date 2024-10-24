using Library.BusinessAccess.Models.Author;

namespace Library.BusinessAccess.UseCases.Authors;

public interface IGetAuthorsByPageUseCase
{
    Task<IEnumerable<AuthorResponseDto>> ExecuteAsync(int page, int pageSize,
        CancellationToken cancellationToken);
}