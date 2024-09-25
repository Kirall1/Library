using Microsoft.AspNetCore.Identity;

namespace Library.DataAccess.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
