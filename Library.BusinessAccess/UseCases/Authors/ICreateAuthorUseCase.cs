using Library.BusinessAccess.Models.Author;

namespace Library.BusinessAccess.UseCases.Authors;

public interface ICreateAuthorUseCase
{
    Task<AuthorCreateResponseDto> ExecuteAsync(AuthorCreateDto authorCreateDto, CancellationToken cancellationToken);
}