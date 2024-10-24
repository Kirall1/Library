using AutoMapper;
using Library.BusinessAccess.Exceptions;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Author;
using Library.BusinessAccess.UseCases.Authors;
using Library.Domain;
using Library.Shared;

namespace Library.BusinessAccess.Services.Impl
{
    public class AuthorService : IAuthorService
    {
        private readonly ICreateAuthorUseCase _createAuthorUseCase;
        private readonly IDeleteAuthorUseCase _deleteAuthorUseCase;
        private readonly IGetAuthorByIdUseCase _getAuthorByIdUseCase;
        private readonly IGetAuthorsByPageUseCase _getAuthorsByPageUseCase;
        private readonly IGetAuthorsUseCase _getAuthorsUseCase;
        private readonly IUpdateAuthorUseCase _updateAuthorUseCase;

        public AuthorService(
            ICreateAuthorUseCase createAuthorUseCase,
            IDeleteAuthorUseCase deleteAuthorUseCase,
            IGetAuthorByIdUseCase getAuthorByIdUseCase,
            IGetAuthorsByPageUseCase getAuthorsByPageUseCase,
            IGetAuthorsUseCase getAuthorsUseCase,
            IUpdateAuthorUseCase updateAuthorUseCase)
        {
            _createAuthorUseCase = createAuthorUseCase;
            _deleteAuthorUseCase = deleteAuthorUseCase;
            _getAuthorByIdUseCase = getAuthorByIdUseCase;
            _getAuthorsByPageUseCase = getAuthorsByPageUseCase;
            _getAuthorsUseCase = getAuthorsUseCase;
            _updateAuthorUseCase = updateAuthorUseCase;
        }

        public async Task<IEnumerable<AuthorResponseDto>> GetAuthorsAsync(CancellationToken cancellationToken)
        {
            return await _getAuthorsUseCase.ExecuteAsync(cancellationToken);
        }

        public async Task<IEnumerable<AuthorResponseDto>> GetAuthorsByPageAsync(int page, int pageSize,
            CancellationToken cancellationToken)
        {
            return await _getAuthorsByPageUseCase.ExecuteAsync(page, pageSize, cancellationToken);
        }

        public async Task<AuthorResponseDto> GetAuthorByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _getAuthorByIdUseCase.ExecuteAsync(id, cancellationToken);
        }

        public async Task<AuthorCreateResponseDto> CreateAuthorAsync(AuthorCreateDto authorCreateDto, 
            CancellationToken cancellationToken)
        {
            return await _createAuthorUseCase.ExecuteAsync(authorCreateDto, cancellationToken);
        }

        public async Task<BaseResponseDto> UpdateAuthorAsync(AuthorUpdateDto authorUpdateDto, 
            CancellationToken cancellationToken)
        {
            return await _updateAuthorUseCase.ExecuteAsync(authorUpdateDto, cancellationToken);
        }

        public async Task<BaseResponseDto> DeleteAuthorAsync(int id, CancellationToken cancellationToken)
        {
            return await _deleteAuthorUseCase.ExecuteAsync(id, cancellationToken);
        }
    }
}