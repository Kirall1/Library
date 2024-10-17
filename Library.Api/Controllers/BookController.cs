using Library.BusinessAccess.Models.Book;
using Microsoft.AspNetCore.Mvc;
using Library.BusinessAccess.Services;
using Microsoft.AspNetCore.Authorization;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookBaseResponseDto>>> GetBooks(CancellationToken cancellationToken)
        {
            return Ok(await _bookService.GetBooksAsync(cancellationToken));
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<BookBaseResponseDto>>> GetBooksByPage(int page, int pageSize,
            CancellationToken cancellationToken)
        {
            return Ok(await _bookService.GetBooksByPageAsync(page, pageSize, cancellationToken));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDetailedResponseDto>> GetBookById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _bookService.GetBookByIdAsync(id, cancellationToken));
        }

        [HttpGet("isbn/{isbn}")]
        public async Task<ActionResult<BookBaseResponseDto>> GetBookByIsbn(string isbn, CancellationToken cancellationToken)
        {
            return Ok(await _bookService.GetBookByIsbnAsync(isbn, cancellationToken));
        }

        [HttpGet("author/{id:int}")]
        public async Task<ActionResult<IEnumerable<BookBaseResponseDto>>> GetBooksByAuthor(int id, 
            CancellationToken cancellationToken)
        {
            return Ok(await _bookService.GetBooksByAuthorAsync(id, cancellationToken));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<BookCreateResponseDto>> CreateBook([FromForm] BookCreateDto bookCreateDto,
            CancellationToken cancellationToken)
        {
            return Ok(await _bookService.AddBookAsync(bookCreateDto, cancellationToken));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut]
        public async Task<ActionResult<BookBaseResponseDto>> UpdateBook([FromForm] BookUpdateDto bookUpdateDto,
            CancellationToken cancellationToken)
        {
            return Ok(await _bookService.EditBookAsync(bookUpdateDto, cancellationToken));
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BookBaseResponseDto>> DeleteBook(int id, CancellationToken cancellationToken)
        {
            return Ok(await _bookService.DeleteBookAsync(id, cancellationToken));
        }

        [Authorize(Policy = "AuthenticatedUser")]
        [HttpPut("take")]
        public async Task<ActionResult<BookTakeReturnDto>> TakeBook([FromBody] BookTakeReturnDto bookTakeReturnDto,
            CancellationToken cancellationToken)
        {
            return Ok(await _bookService.TakeBookAsync(bookTakeReturnDto, cancellationToken));
        }

        [Authorize(Policy = "AuthenticatedUser")]
        [HttpPut("return")]
        public async Task<ActionResult<BookTakeReturnDto>> ReturnBook([FromBody] BookTakeReturnDto bookTakeReturnDto,
            CancellationToken cancellationToken)
        {
            return Ok(await _bookService.ReturnBookAsync(bookTakeReturnDto, cancellationToken));
        }
    }
}
