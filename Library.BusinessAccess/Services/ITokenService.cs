using System.Security.Claims;
using Library.BusinessAccess.Models;

namespace Library.BusinessAccess.Services
{
    public interface ITokenService
    {
        public string GenerateAccessToken(IEnumerable<Claim> claims);
        public string GenerateRefreshToken();
        public Task<TokenApiModel> RefreshAsync(TokenApiModel tokenApiModel);
        public Task RevokeAsync(TokenApiModel tokenApiModel);
    }
}