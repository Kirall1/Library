using AutoMapper;
using Library.BusinessAccess.Exceptions;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Author;

namespace Library.BusinessAccess.UseCases.Authors.Impl;

public class UpdateAuthorUseCase : IUpdateAuthorUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAuthorUseCase(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponseDto> ExecuteAsync(AuthorUpdateDto authorUpdateDto,
        CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(authorUpdateDto.Id, cancellationToken);
        if (author == null)
            throw new NotFoundException("Author not found");
        _mapper.Map(authorUpdateDto, author);
        var updatedAuthor = _unitOfWork.Authors.Update(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new BaseResponseDto() { Id = updatedAuthor.Id };
    }
}