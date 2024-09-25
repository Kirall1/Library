using System.Security.Claims;
using Library.Business.Models;

namespace Library.Business.Services
{
    public interface ITokenService
    {
        public string GenerateAccessToken(IEnumerable<Claim> claims);
        public string GenerateRefreshToken();
        public Task<TokenApiModel> RefreshAsync(TokenApiModel tokenApiModel);
        public Task RevokeAsync(TokenApiModel tokenApiModel);
    }
}