using Library.BusinessAccess.Models.Author;
using Microsoft.AspNetCore.Mvc;
using Library.BusinessAccess.Services;
using Library.BusinessAccess.Models;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorResponseDto>>> GetAuthors(CancellationToken cancellationToken)
        {
            return Ok(await _authorService.GetAuthorsAsync(cancellationToken));
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<AuthorResponseDto>>> GetAuthorsByPage(int page, int pageSize,
        CancellationToken cancellationToken)
        {
            return Ok(await _authorService.GetAuthorsByPageAsync(page, pageSize, cancellationToken));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorResponseDto>> GetAuthorById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _authorService.GetAuthorByIdAsync(id, cancellationToken));
        }


        [HttpPost]
        public async Task<ActionResult<AuthorCreateResponseDto>> CreateAuthor([FromForm] AuthorCreateDto author,
            CancellationToken cancellationToken)
        {
            return Ok(await _authorService.CreateAuthorAsync(author, cancellationToken));
        }



        [HttpPut]
        public async Task<ActionResult<BaseResponseDto>> UpdateAuthor([FromForm] AuthorUpdateDto author,
            CancellationToken cancellationToken)
        {
            return Ok(await _authorService.UpdateAuthorAsync(author, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteAuthor(int id, CancellationToken cancellationToken)
        {
            return Ok(await _authorService.DeleteAuthorAsync(id, cancellationToken));
        }
    }
}
