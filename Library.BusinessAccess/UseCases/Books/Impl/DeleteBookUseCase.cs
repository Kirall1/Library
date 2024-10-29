using Library.BusinessAccess.Exceptions;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Services;

namespace Library.BusinessAccess.UseCases.Books.Impl;

public class DeleteBookUseCase : IDeleteBookUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

    public DeleteBookUseCase(IUnitOfWork unitOfWork, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }

    public async Task<BaseResponseDto> ExecuteAsync(int id, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(id, cancellationToken);
        if (book == null)
            throw new NotFoundException("Book not found");
        if (book.Image != null)
            _fileService.DeleteFile(book.Image);

        _unitOfWork.Books.Delete(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new BaseResponseDto() { Id = id };
    }
}
