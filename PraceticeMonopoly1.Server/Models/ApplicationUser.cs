using Microsoft.AspNetCore.Identity;

namespace PraceticeMonopoly1.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
