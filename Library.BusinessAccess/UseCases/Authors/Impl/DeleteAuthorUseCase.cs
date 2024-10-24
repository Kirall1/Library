using Library.BusinessAccess.Exceptions;
using Library.BusinessAccess.Models;
using Library.Shared;

namespace Library.BusinessAccess.UseCases.Authors.Impl;

public class DeleteAuthorUseCase : IDeleteAuthorUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuthorUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponseDto> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(id, cancellationToken);
        if (author == null)
            throw new NotFoundException("Author not found");
        _unitOfWork.Authors.Delete(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new BaseResponseDto() { Id = id };
    }
}