using Library.Business.Models;
using Library.Business.Models.Book;

namespace Library.Business.Services
{
    public interface IBookService
    {
        public Task<IEnumerable<BookBaseResponseDto>> GetBooksAsync(CancellationToken cancellationToken);
        public Task<IEnumerable<BookBaseResponseDto>> GetBooksByPageAsync(int page, int pageSize,
            CancellationToken cancellationToken);
        public Task<BookDetailedResponseDto> GetBookByIdAsync(int id, CancellationToken cancellationToken);
        public Task<BookBaseResponseDto> GetBookByIsbnAsync(string isbn, CancellationToken cancellationToken);
        public Task<BookCreateResponseDto> AddBookAsync(BookCreateDto bookToCreate,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> EditBookAsync(BookUpdateDto bookToUpdate,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> DeleteBookAsync(int id, CancellationToken cancellationToken);
        public Task<BaseResponseDto> TakeBookAsync(BookTakeReturnDto bookToTake,
            CancellationToken cancellationToken);
        public Task<BaseResponseDto> ReturnBookAsync(BookTakeReturnDto bookToReturn,
            CancellationToken cancellationToken);
        public Task<IEnumerable<BookBaseResponseDto>> GetBooksByAuthorAsync(int authorId,
            CancellationToken cancellationRoken);
    }
}
