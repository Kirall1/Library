using AutoMapper;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Author;

namespace Library.BusinessAccess.UseCases.Authors.Impl;

public class GetAuthorsByPageUseCase : IGetAuthorsByPageUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorsByPageUseCase(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<AuthorResponseDto>> ExecuteAsync(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var authors = await _unitOfWork.Authors.GetByPageAsync(page, pageSize, cancellationToken);
        return _mapper.Map<IEnumerable<AuthorResponseDto>>(authors);
    }
}