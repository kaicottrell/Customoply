using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CustomMonopoly.Server.Services;
using CustomMonopoly.Server.Models;
using System.Security.Claims;
using CustomMonopoly.Server.ViewModels;

namespace CustomMonopoly.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GameService _gameService;
        private readonly GameEventHandlingService _gameEventHandlingService;
        //Inject database context
        public GameController(UserManager<ApplicationUser> userManager, GameService gameService, GameEventHandlingService gameEventHandlingService)
        {
            _userManager = userManager;
            _gameService = gameService;
            _gameEventHandlingService = gameEventHandlingService;
        }

        [HttpPost("MovePlayer")]
        public IActionResult MovePlayer(int gameId)
        {
            //Get the user's id
            var userId = _userManager.GetUserId(User);

            var boardEvent = _gameService.MovePlayer(userId, gameId);
            return Ok(boardEvent);
        }
        [HttpPost("HandleBoardEventResponse")]
        public IActionResult HandleBoardEventResponse([FromBody] BoardEventResponse boardEventResponse)
        {
            if (boardEventResponse.IsAcknowledged)
            {
                switch (boardEventResponse.BoardEvent)
                {
                    case AvailableForPurchaseEvent availableForPurchaseEvent:
                        if (boardEventResponse is AvailableForPurchaseEventResponse propertyPurchaseResponse)
                        {
                            _gameEventHandlingService.HandleAvailableForPurchaseEvent(availableForPurchaseEvent, propertyPurchaseResponse.PropertyOptionType);
                            return Ok();
                        }
                        else
                        {
                            return BadRequest("Event Response Mismatch");
                        }
                    case HomeNoActionEvent:
                        return Ok();
                    case RentRequiredEvent rentRequiredEvent:
                        _gameEventHandlingService.HandleRentRequiredEvent(rentRequiredEvent);
                        return Ok("Rent Successfully Paid");
                    //TODO: Handle to Jail event, card events etc.

                }
                return Ok("Event Response Handled Successfully");

            }
            //TODO: Switch player turn
            return BadRequest("Board Event not successfully acknowledged");
        }
  
        //[HttpGet("GetGameBoard")]
        //public IActionResult GetGameBoard()
        //{
        //    //return Ok(_gameService.GetGameBoard());
        //}
    }
}
