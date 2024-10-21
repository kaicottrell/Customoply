using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CustomMonopoly.Server.Services;
using CustomMonopoly.Server.Models;
using System.Security.Claims;

namespace CustomMonopoly.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GameService _gameService;
        //Inject database context
        public GameController(UserManager<ApplicationUser> userManager, GameService gameService)
        {
            _userManager = userManager;
            _gameService = gameService;
        }

        [HttpPost("MovePlayer")]
        public IActionResult MovePlayer(int gameId)
        {
            //Get the user's id
            var userId = _userManager.GetUserId(User);

            var propertyAndAvailability = _gameService.MovePlayer(userId, gameId);
            return Ok(propertyAndAvailability);
        }
        [HttpPost("BuyProperty")]
        public IActionResult BuyProperty(int propertyId, int gameId)
        {
            //Get the user's id
            var userId = _userManager.GetUserId(User);

            var (success, message) = _gameService.BuyProperty(propertyId, userId, gameId);

            if (success)
            {
                return Ok(message);
            }
            else
            {
                return BadRequest(message);
            }
        }
        //[HttpGet("GetGameBoard")]
        //public IActionResult GetGameBoard()
        //{
        //    //return Ok(_gameService.GetGameBoard());
        //}
    }
}
