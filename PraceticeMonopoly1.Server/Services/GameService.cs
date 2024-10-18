using Microsoft.OpenApi.Services;
using PraceticeMonopoly1.Server.Models;
using PraceticeMonopoly1.Server.Models.BoardSquares;

namespace PraceticeMonopoly1.Server.Services
{
    /// <summary>
    /// Handles services related to the monopoly game
    /// </summary>
    public class GameService
    {
        private readonly Game _game;
        private readonly ApplicationDbContext _db;
        public GameService(Game game, ApplicationDbContext db) {
            _game = game;
            _db = db;
        }

        //Sets up the board 
        public PropertyAndAvailability MovePlayer(Player player, int gameId)
        {
            // move the player to another spot on the board
            player.CurrentPostion = ((player.CurrentPostion + player.Roll2Dice()) % _game.Board.Count());
            // Get the boardsquare at the new postion and return type
            //Return the property and if it is available for purchase
            BoardSquare boardSquare = _game.Board[player.CurrentPostion];
            //TODO: Add the ability to charge the players rent 
            if (boardSquare is PropertySquare)
            {
                bool isAvailable = _db.PlayerProperties.Any(pp => pp.PropertyId == boardSquare.Id && pp.Player.Game.Id == gameId) == false;
                return new PropertyAndAvailability { Property = (PropertySquare)boardSquare, IsAvailable = isAvailable };
            }   
            else
            {
                return new PropertyAndAvailability { Property = null, IsAvailable = false };
            }
        }
        public (bool success, string message) BuyProperty(int propertyId, int playerId, int gameId)
        {
            //Verify that the property is not already bought
            bool canBuy = _db.PlayerProperties.Any(p => p.PropertyId == propertyId && p.Player.Game.Id == gameId) == false;
            if (canBuy)
            {
                // add the property to a players list
                _db.PlayerProperties.Add(new PlayerProperty { PlayerId = playerId, PropertyId = propertyId });
                _db.SaveChanges();
                return (true, "Property Successfully bought!");
            }
            else
            {
                return (false, "Property is not available");
            }
        }
        public List<BoardSquare> GetGameBoard()
        {
        }

        //public string HandleOutcome()
        //{

        //}
        //public string HandleChoice()
        //{

        //}
        public struct PropertyAndAvailability()
        {
            public PropertySquare? Property { get; set; }
            public bool IsAvailable { get; set; }
        }
    }
}
