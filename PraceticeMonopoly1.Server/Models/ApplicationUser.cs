using Microsoft.AspNetCore.Identity;

namespace CustomMonopoly.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
