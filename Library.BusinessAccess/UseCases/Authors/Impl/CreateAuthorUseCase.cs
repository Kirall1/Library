using AutoMapper;
using Library.BusinessAccess.Models.Author;
using Library.Domain;
using Library.Shared;

namespace Library.BusinessAccess.UseCases.Authors.Impl;

public class CreateAuthorUseCase : ICreateAuthorUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuthorUseCase(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthorCreateResponseDto> ExecuteAsync(AuthorCreateDto authorCreateDto,
        CancellationToken cancellationToken)
    {
        var author = _mapper.Map<Author>(authorCreateDto);
        var createdAuthor = await _unitOfWork.Authors.AddAsync(author, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<AuthorCreateResponseDto>(createdAuthor);
    }
}