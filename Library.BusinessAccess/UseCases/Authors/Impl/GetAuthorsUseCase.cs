using AutoMapper;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Author;

namespace Library.BusinessAccess.UseCases.Authors.Impl;

public class GetAuthorsUseCase : IGetAuthorsUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorsUseCase(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<AuthorResponseDto>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var authors = await _unitOfWork.Authors.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<AuthorResponseDto>>(authors);
    }
}