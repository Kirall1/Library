using Library.BusinessAccess.Models.Author;

namespace Library.BusinessAccess.UseCases.Authors;

public interface IGetAuthorsUseCase
{
    Task<IEnumerable<AuthorResponseDto>> ExecuteAsync(CancellationToken cancellationToken);
}