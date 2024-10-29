using Microsoft.AspNetCore.Identity;

namespace Library.BusinessAccess.Models.User
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
