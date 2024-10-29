using AutoMapper;
using Library.BusinessAccess.Exceptions;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Author;

namespace Library.BusinessAccess.UseCases.Authors.Impl;

public class GetAuthorByIdUseCase : IGetAuthorByIdUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorByIdUseCase(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AuthorResponseDto> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(id, cancellationToken);
        if (author == null)
            throw new NotFoundException("Author not found");
        return _mapper.Map<AuthorResponseDto>(author);
    }
}