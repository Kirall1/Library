using Library.BusinessAccess.Models.Book;
using Microsoft.AspNetCore.Mvc;
using Library.BusinessAccess.UseCases.Books;
using Microsoft.AspNetCore.Authorization;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
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

        public BookController(
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookBaseResponseDto>>> GetBooks(CancellationToken cancellationToken)
        {
            return Ok(await _getBooksUseCase.ExecuteAsync(cancellationToken));
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<BookBaseResponseDto>>> GetBooksByPage(int page, int pageSize,
            CancellationToken cancellationToken)
        {
            return Ok(await _getBooksByPageUseCase.ExecuteAsync(page, pageSize, cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDetailedResponseDto>> GetBookById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _getBookByIdUseCase.ExecuteAsync(id, cancellationToken));
        }

        [HttpGet("isbn/{isbn}")]
        public async Task<ActionResult<BookBaseResponseDto>> GetBookByIsbn(string isbn, CancellationToken cancellationToken)
        {
            return Ok(await _getBookByIsbnUseCase.ExecuteAsync(isbn, cancellationToken));
        }

        [HttpGet("author/{id:int}")]
        public async Task<ActionResult<IEnumerable<BookBaseResponseDto>>> GetBooksByAuthor(int id, 
            CancellationToken cancellationToken)
        {
            return Ok(await _getBooksByAuthorUseCase.ExecuteAsync(id, cancellationToken));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<BookCreateResponseDto>> AddBook([FromForm] BookCreateDto bookCreateDto,
            CancellationToken cancellationToken)
        {
            return Ok(await _addBookUseCase.ExecuteAsync(bookCreateDto, cancellationToken));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut]
        public async Task<ActionResult<BookBaseResponseDto>> EditBook([FromForm] BookUpdateDto bookUpdateDto,
            CancellationToken cancellationToken)
        {
            return Ok(await _editBookUseCase.ExecuteAsync(bookUpdateDto, cancellationToken));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BookBaseResponseDto>> DeleteBook(int id, CancellationToken cancellationToken)
        {
            return Ok(await _deleteBookUseCase.ExecuteAsync(id, cancellationToken));
        }

        [Authorize(Policy = "AuthenticatedUser")]
        [HttpPut("take")]
        public async Task<ActionResult<BookTakeReturnDto>> TakeBook([FromBody] BookTakeReturnDto bookTakeReturnDto,
            CancellationToken cancellationToken)
        {
            return Ok(await _takeBookUseCase.ExecuteAsync(bookTakeReturnDto, cancellationToken));
        }

        [Authorize(Policy = "AuthenticatedUser")]
        [HttpPut("return")]
        public async Task<ActionResult<BookTakeReturnDto>> ReturnBook([FromBody] BookTakeReturnDto bookTakeReturnDto,
            CancellationToken cancellationToken)
        {
            return Ok(await _returnBookUseCase.ExecuteAsync(bookTakeReturnDto, cancellationToken));
        }
    }
}
