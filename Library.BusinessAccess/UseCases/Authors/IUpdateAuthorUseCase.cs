using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Author;

namespace Library.BusinessAccess.UseCases.Authors;

public interface IUpdateAuthorUseCase
{
    Task<BaseResponseDto> ExecuteAsync(AuthorUpdateDto authorUpdateDto, CancellationToken cancellationToken);
}