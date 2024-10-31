using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CustomMonopoly.Server.Services;
using CustomMonopoly.Server.Models;
using System.Security.Claims;
using CustomMonopoly.Server.ViewModels;
using Microsoft.AspNetCore.Authorization;
using CustomMonopoly.Server.Extensions;
using CustomMonopoly.Server.Config;
using CustomMonopoly.Server.ViewModels.DTOs.Responses;

namespace CustomMonopoly.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
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
        [HttpPost("StartAndGetGame")]
        public IActionResult StartAndGetGame([FromBody] StartGameRequestVM startGameRequestVM)
        {
            //Get the user's id
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            //Create new game
            var newGame = _gameService.StartGame();
            //Create a new player for the game
            var players = _gameService.ConfigurePlayersForGame(newGame, userId, startGameRequestVM.NumberOfPlayers);
            newGame.Players = players;

            return Ok(newGame.ToGameDTO());
        }
        [HttpGet("GetExistingGame")]
        public IActionResult GetExistingGame()
        {
            //Get the user's id
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            var game = _gameService.GetExistingGame(userId);
            var gameDTO = game?.ToGameDTO();
            return gameDTO != null ? Ok(gameDTO) : NoContent();
        }
        [HttpPost("MovePlayer")]
        public IActionResult MovePlayer([FromQuery] int gameId)
        {
            var gameWithEventDTO = _gameService.MovePlayer(gameId);
            return Ok(gameWithEventDTO);
        }

        [HttpPost("HandleBoardEventResponse")]
        public IActionResult HandleBoardEventResponse([FromBody] BoardEventResponse boardEventResponse)
        {
            // Get the user's id
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            try
            {
                string eventType = boardEventResponse.BoardEvent.EventType;

                // Inline shorthand switch statement
                Action boardEventAction = eventType switch
                {
                    SD.PurchaseOrAuctionPropertyBoardEvent => () => _gameEventHandlingService.HandleAvailableForPurchaseEvent(boardEventResponse.BoardEvent, boardEventResponse.SelectedPropertyOption),
                    SD.RentRequiredBoardEvent => () => _gameEventHandlingService.HandleRentRequiredEvent(boardEventResponse.BoardEvent),
                    _ => () => { } // No-op for default case
                };

                // Execute the action
                boardEventAction();

                _gameService.UpdateToNextPlayerTurn(boardEventResponse.GameId);
                var game = _gameService.GetExistingGame(userId);
                if (game == null)
                {
                    return BadRequest("Game no longer found");
                }

                return Ok(game.ToGameDTO());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("GetGameBoard")]
        //public IActionResult GetGameBoard()
        //{
        //    //return Ok(_gameService.GetGameBoard());
        //}
    }
}
