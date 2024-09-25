using AutoMapper;
using FluentValidation;
using Library.Business.Exceptions;
using Library.Business.Models;
using Library.Business.Models.Author;
using Library.DataAccess;
using Library.DataAccess.Entities;

namespace Library.Business.Services.Impl
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper, IValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<IEnumerable<AuthorResponseDto>> GetAuthorsAsync(CancellationToken cancellationToken)
        {
            var authors = await _unitOfWork.Authors.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<AuthorResponseDto>>(authors);
        }

        public async Task<IEnumerable<AuthorResponseDto>> GetAuthorsByPageAsync(int page, int pageSize,
            CancellationToken cancellationToken)
        {
            var authors = await _unitOfWork.Authors.GetByPageAsync(page, pageSize, cancellationToken);
            return _mapper.Map<IEnumerable<AuthorResponseDto>>(authors);
        }

        public async Task<AuthorResponseDto> GetAuthorByIdAsync(int id, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id, cancellationToken);
            if (author == null)
                throw new NotFoundException("Author not found");
            return _mapper.Map<AuthorResponseDto>(author);
        }

        public async Task<AuthorCreateResponseDto> CreateAuthorAsync(AuthorCreateDto authorCreateDto, CancellationToken cancellationToken)
        {
            await _validationService.ValidateAsync(authorCreateDto, cancellationToken);
            var author = _mapper.Map<Author>(authorCreateDto);
            var createdAuthor = await _unitOfWork.Authors.AddAsync(author, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<AuthorCreateResponseDto>(createdAuthor);
        }

        public async Task<BaseResponseDto> UpdateAuthorAsync(AuthorUpdateDto authorUpdateDto, CancellationToken cancellationToken)
        {
            await _validationService.ValidateAsync(authorUpdateDto, cancellationToken);
            var author = await _unitOfWork.Authors.GetByIdAsync(authorUpdateDto.Id, cancellationToken);
            if (author == null)
                throw new NotFoundException("Author not found");
            _mapper.Map(authorUpdateDto, author);
            var updatedAuthor = _unitOfWork.Authors.Update(author);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = updatedAuthor.Id };
        }

        public async Task<BaseResponseDto> DeleteAuthorAsync(int id, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id, cancellationToken);
            if (author == null)
                throw new NotFoundException("Author not found");
            _unitOfWork.Authors.Delete(author);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new BaseResponseDto() { Id = id };
        }
    }
}