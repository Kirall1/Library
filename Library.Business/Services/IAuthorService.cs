using Library.Business.Models;
using Library.Business.Models.Author;

namespace Library.Business.Services
{
    public interface IAuthorService
    {
        public Task<IEnumerable<AuthorResponseDto>> GetAuthorsAsync(CancellationToken cancellationToken);
        public Task<IEnumerable<AuthorResponseDto>> GetAuthorsByPageAsync(int page, int pageSize,
            CancellationToken cancellationToken);
        public Task<AuthorResponseDto> GetAuthorByIdAsync(int id, CancellationToken cancellationToken);
        public Task<AuthorCreateResponseDto> CreateAuthorAsync(AuthorCreateDto authorCreateDto,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> UpdateAuthorAsync(AuthorUpdateDto authorUpdateDto,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> DeleteAuthorAsync(int id, CancellationToken cancellationToken);
    }
}