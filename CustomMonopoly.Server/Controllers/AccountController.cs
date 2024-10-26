using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CustomMonopoly.Server.ViewModels.Authentication;
using Microsoft.AspNetCore.Identity;
using CustomMonopoly.Server.Models;

namespace CustomMonopoly.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet("PingAuth")]
        public IActionResult PingAuth()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return Unauthorized();
            }
            return Ok(new {Email = email});
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }
            var applicationUser = new ApplicationUser
            {
                UserName = registerVM.Email,
                Email = registerVM.Email
            };
            
            var result = await _userManager.CreateAsync(applicationUser, registerVM.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(new { Errors = errors });
            }
        }
    }
}
