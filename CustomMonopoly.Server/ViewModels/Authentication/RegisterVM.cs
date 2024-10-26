using System.ComponentModel.DataAnnotations;

namespace CustomMonopoly.Server.ViewModels.Authentication
{
    public class RegisterVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
