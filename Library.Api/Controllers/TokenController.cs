using Microsoft.AspNetCore.Mvc;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Services;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpPost]
        [Route("refresh")]
        public async Task<TokenApiModel> Refresh(TokenApiModel tokenApiModel)
        {
            return await _tokenService.RefreshAsync(tokenApiModel);
        }

        [HttpPost]
        [Route("revoke")]
        public async Task Revoke(TokenApiModel tokenApiModel)
        {
            await _tokenService.RevokeAsync(tokenApiModel);
        }
    }
}