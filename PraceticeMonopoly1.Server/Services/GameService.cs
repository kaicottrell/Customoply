using Microsoft.OpenApi.Services;
using CustomMonopoly.Server.Models;
using CustomMonopoly.Server.Models.BoardSquares;
using CustomMonopoly.Server.Data;

namespace CustomMonopoly.Server.Services
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

        public PropertyAndAvailability MovePlayer(string userId, int gameId)
        {
            //Get the player associated with the userId
            var player = _db.Players.Where(player => player.UserId == userId).First();

            // move the player to another spot on the board
            player.CurrentPostion = ((player.CurrentPostion + player.RollTwoDice()) % _game.Board.Count());
            // Get the boardsquare at the new postion and return type
            //Return the property and if it is available for purchase
            BoardSquare boardSquare = _game.Board[player.CurrentPostion];
            //TODO: Add the ability to charge the players rent 
            if (boardSquare is PropertySquare)
            {
                bool isAvailable = _db.PlayerProperties.Any(pp => pp.PropertySquareId == boardSquare.Id && pp.Player.Game.Id == gameId) == false;
                return new PropertyAndAvailability { Property = (PropertySquare)boardSquare, IsAvailable = isAvailable };
            }   
            else
            {
                return new PropertyAndAvailability { Property = null, IsAvailable = false };
            }
        }
        public (bool success, string message) BuyProperty(int propertyId, string userId, int gameId)
        {
            //Get the player associated with the userId
            var player = _db.Players.Where(player => player.UserId == userId).First();

            //Verify that the property is not already bought
            bool canBuy = _db.PlayerProperties.Any(p => p.PropertySquareId == propertyId && p.Player.Game.Id == gameId) == false;
            if (canBuy)
            {
                // add the property to a players list
                _db.PlayerProperties.Add(new PlayerProperty { PlayerId = player.Id, PropertySquareId = propertyId });
                _db.SaveChanges();
                return (true, "Property Successfully bought!");
            }
            else
            {
                return (false, "Property is not available");
            }
        }
        //public List<BoardSquare> GetGameBoard()
        //{
        //}

        //public string HandleOutcome()
        //{

        //}
        //public string HandleChoice()
        //{

        //}

        //Sets up the board spaces, sets up players
        public void ConfigureGame()
        {

        }

        public struct PropertyAndAvailability()
        {
            public PropertySquare? Property { get; set; }
            public bool IsAvailable { get; set; }
        }
    }
}
