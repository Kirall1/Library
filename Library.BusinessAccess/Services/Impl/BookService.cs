using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.Book;
using Library.BusinessAccess.UseCases.Books;

namespace Library.BusinessAccess.Services.Impl
{
    public class BookService : IBookService
    {
        private readonly IGetBooksUseCase _getBooksUseCase;
        private readonly IGetBookByIdUseCase _getBookByIdUseCase;
        private readonly IAddBookUseCase _addBookUseCase;
        private readonly IEditBookUseCase _editBookUseCase;
        private readonly IDeleteBookUseCase _deleteBookUseCase;
        private readonly ITakeBookUseCase _takeBookUseCase;
        private readonly IReturnBookUseCase _returnBookUseCase;
        private readonly IGetBooksByAuthorUseCase _getBooksByAuthorUseCase;
        private readonly IGetBooksByPageUseCase _getBooksByPageUseCase;
        private readonly IGetBookByIsbnUseCase _getBookByIsbnUseCase;

        public BookService(
            IGetBooksUseCase getBooksUseCase,
            IGetBookByIdUseCase getBookByIdUseCase,
            IAddBookUseCase addBookUseCase,
            IEditBookUseCase editBookUseCase,
            IDeleteBookUseCase deleteBookUseCase,
            ITakeBookUseCase takeBookUseCase,
            IReturnBookUseCase returnBookUseCase,
            IGetBooksByAuthorUseCase getBooksByAuthorUseCase,
            IGetBooksByPageUseCase getBooksByPageUseCase,
            IGetBookByIsbnUseCase getBookByIsbnUseCase)
        {
            _getBooksUseCase = getBooksUseCase;
            _getBookByIdUseCase = getBookByIdUseCase;
            _addBookUseCase = addBookUseCase;
            _editBookUseCase = editBookUseCase;
            _deleteBookUseCase = deleteBookUseCase;
            _takeBookUseCase = takeBookUseCase;
            _returnBookUseCase = returnBookUseCase;
            _getBooksByAuthorUseCase = getBooksByAuthorUseCase;
            _getBooksByPageUseCase = getBooksByPageUseCase;
            _getBookByIsbnUseCase = getBookByIsbnUseCase;
        }

        public Task<IEnumerable<BookBaseResponseDto>> GetBooksAsync(CancellationToken cancellationToken)
        {
            return _getBooksUseCase.ExecuteAsync(cancellationToken);
        }

        public Task<IEnumerable<BookBaseResponseDto>> GetBooksByPageAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            return _getBooksByPageUseCase.ExecuteAsync(page, pageSize, cancellationToken);
        }

        public Task<BookDetailedResponseDto> GetBookByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _getBookByIdUseCase.ExecuteAsync(id, cancellationToken);
        }

        public Task<BookBaseResponseDto> GetBookByIsbnAsync(string isbn, CancellationToken cancellationToken)
        {
            return _getBookByIsbnUseCase.ExecuteAsync(isbn, cancellationToken);
        }

        public Task<BookCreateResponseDto> AddBookAsync(BookCreateDto bookToCreate, CancellationToken cancellationToken)
        {
            return _addBookUseCase.ExecuteAsync(bookToCreate, cancellationToken);
        }

        public Task<BaseResponseDto> EditBookAsync(BookUpdateDto bookToUpdate, CancellationToken cancellationToken)
        {
            return _editBookUseCase.ExecuteAsync(bookToUpdate, cancellationToken);
        }

        public Task<BaseResponseDto> DeleteBookAsync(int id, CancellationToken cancellationToken)
        {
            return _deleteBookUseCase.ExecuteAsync(id, cancellationToken);
        }

        public Task<BaseResponseDto> TakeBookAsync(BookTakeReturnDto bookToTake, CancellationToken cancellationToken)
        {
            return _takeBookUseCase.ExecuteAsync(bookToTake, cancellationToken);
        }

        public Task<BaseResponseDto> ReturnBookAsync(BookTakeReturnDto bookToReturn, CancellationToken cancellationToken)
        {
            return _returnBookUseCase.ExecuteAsync(bookToReturn, cancellationToken);
        }

        public Task<IEnumerable<BookBaseResponseDto>> GetBooksByAuthorAsync(int authorId, CancellationToken cancellationToken)
        {
            return _getBooksByAuthorUseCase.ExecuteAsync(authorId, cancellationToken);
        }
    }

}
