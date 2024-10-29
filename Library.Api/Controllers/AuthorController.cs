using Library.BusinessAccess.Models.Author;
using Microsoft.AspNetCore.Mvc;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.UseCases.Authors;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ICreateAuthorUseCase _createAuthorUseCase;
        private readonly IDeleteAuthorUseCase _deleteAuthorUseCase;
        private readonly IGetAuthorByIdUseCase _getAuthorByIdUseCase;
        private readonly IGetAuthorsByPageUseCase _getAuthorsByPageUseCase;
        private readonly IGetAuthorsUseCase _getAuthorsUseCase;
        private readonly IUpdateAuthorUseCase _updateAuthorUseCase;

        public AuthorController(
            ICreateAuthorUseCase createAuthorUseCase,
            IDeleteAuthorUseCase deleteAuthorUseCase,
            IGetAuthorByIdUseCase getAuthorByIdUseCase,
            IGetAuthorsByPageUseCase getAuthorsByPageUseCase,
            IGetAuthorsUseCase getAuthorsUseCase,
            IUpdateAuthorUseCase updateAuthorUseCase
            )
        {
            _createAuthorUseCase = createAuthorUseCase;
            _deleteAuthorUseCase = deleteAuthorUseCase;
            _getAuthorByIdUseCase = getAuthorByIdUseCase;
            _getAuthorsByPageUseCase = getAuthorsByPageUseCase;
            _getAuthorsUseCase = getAuthorsUseCase;
            _updateAuthorUseCase = updateAuthorUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorResponseDto>>> GetAuthors(CancellationToken cancellationToken)
        {
            return Ok(await _getAuthorsUseCase.ExecuteAsync(cancellationToken));
        }

        
        [HttpGet("{page}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<AuthorResponseDto>>> GetAuthorsByPage(int page, int pageSize,
        CancellationToken cancellationToken)
        {
            return Ok(await _getAuthorsByPageUseCase.ExecuteAsync(page, pageSize, cancellationToken));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorResponseDto>> GetAuthorById(int id, CancellationToken cancellationToken)
        {
            return Ok(await _getAuthorByIdUseCase.ExecuteAsync(id, cancellationToken));
        }


        [HttpPost]
        public async Task<ActionResult<AuthorCreateResponseDto>> CreateAuthor([FromForm] AuthorCreateDto author,
            CancellationToken cancellationToken)
        {
            return Ok(await _createAuthorUseCase.ExecuteAsync(author, cancellationToken));
        }



        [HttpPut]
        public async Task<ActionResult<BaseResponseDto>> UpdateAuthor([FromForm] AuthorUpdateDto author,
            CancellationToken cancellationToken)
        {
            return Ok(await _updateAuthorUseCase.ExecuteAsync(author, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponseDto>> DeleteAuthor(int id, CancellationToken cancellationToken)
        {
            return Ok(await _deleteAuthorUseCase.ExecuteAsync(id, cancellationToken));
        }
    }
}
