using Microsoft.OpenApi.Services;
using CustomMonopoly.Server.Models;
using CustomMonopoly.Server.Models.BoardSquares;
using CustomMonopoly.Server.Data;
using CustomMonopoly.Server.ViewModels;
using CustomMonopoly.Server.Exceptions;

namespace CustomMonopoly.Server.Services
{
    /// <summary>
    /// Interacts directly with the database to process the handling of game
    /// specific  data such as configuring the game and the board, moving players, buying houses, etc.
    /// </summary>
    public class GameService
    {
        private readonly Game _game;
        private readonly ApplicationDbContext _db;
        public GameService(int gameId, ApplicationDbContext db) {
            _db = db;
            _game = _db.Games.Where(game => game.Id == gameId).First();
        }
        /// <summary>
        /// Rolls the players dice, and move the player along the board
        /// </summary>
        /// <param name="userId">The ApplicationUserid of the user making the action</param>
        /// <param name="gameId">The game being affected</param>
        /// <returns>A BoardEvent such as <see cref="AvailableForPurchaseEvent" /> or <see cref="GainMoneyCardEvent" /></returns>
        //TODO: Return a tuple of the GoEvent? find a way to display this on the frontend 
        public BoardEvent MovePlayer(string userId, int gameId)
        {
            //Get the player associated with the userId
            var player = _db.Players.Where(player => player.UserId == userId).First();
            // move the player to another spot on the board
            int diceRoll = player.RollTwoDice();
            int oldPlayerPosition = player.CurrentPostion;
            int newPlayerPosition = ((player.CurrentPostion + diceRoll) % _game.Board.Count());
            //Lapped the board (reset)
            if (oldPlayerPosition + diceRoll > _game.Board.Count())
            {
                player.Balance += 200;
            }
            player.CurrentPostion = newPlayerPosition;
            _db.Update(player);
            _db.SaveChanges();
            // Get the boardsquare at the new postion and return type
            //Return the property and if it is available for purchase
            BoardSquare boardSquare = _game.Board[player.CurrentPostion];
            // We need to be able to handle a wide variety of events,  such as returning views for a card draw,
            // or property options such as pay rent or buy if available, or go to jail or free parking
            switch (boardSquare)
            {
                case PropertySquare propertySquare:
                    // See if PlayerProperty exists
                    var playerProperty = _db.PlayerProperties.Where(pp => pp.PlayerId == player.Id && pp.PropertySquareId == boardSquare.Id).FirstOrDefault();
                    // Based on if the property is owned we return an event
                    if (playerProperty == null)
                    {
                        // return the available for purchase event
                        return new AvailableForPurchaseEvent(propertySquare.Price);
                    }
                    // Owned by the player who landed on the property square
                    else if (playerProperty.PlayerId == player.Id)
                    {
                        return new HomeNoActionEvent();
                    }
                    //Owned by another player -- pay rent
                    else
                    {
                        int rent = DetermineRent(playerProperty, propertySquare);
                        return new RentRequiredEvent(rent, $"Pay {player.Color} Player ${rent}", player.Color);
                    }

                //TODO: Implement the Card Service to handle drawing a random chance or community card 
                //case ChanceSquare chanceSquare:
                //    // Handle chance square logic here
                //    return new CardEvent { CardType = CardType.Chance }; 

                //case CommunityChestSquare communityChestSquare:
                //    // Handle community chest square logic here
                //    break;

                case FreeParkingSquare freeParkingSquare:
                    break;
                case GoToJailSquare goToJailSquare:
                    break;
                case JailSquare jailSquare:
                    break;
                default:
                    // Handle default case if needed
                    break;
            }
            return new HomeNoActionEvent();

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
        /// <summary>
        /// Determines the rent cost based on the number of houses and if there is a hotel
        /// </summary>
        /// <returns>A tuple representing the amount of rent and the color of the player the rent is to</returns>
        private int DetermineRent(PlayerProperty playerProperty, PropertySquare propertySquare)
        {
            if (playerProperty.HotelCount == 1)
            {
                return propertySquare.RentHotel;
            }
            switch (playerProperty.HouseCount)
            {
                case 0:
                    return propertySquare.RentNoHouse;
                case 1:
                    return propertySquare.RentOneHouse;
                case 2:
                    return propertySquare.RentTwoHouse;
                case 3:
                    return propertySquare.RentThreeHouse;
                case 4:
                    return propertySquare.RentFourHouse;
                default:
                    throw new InvalidPropertyHouseCountException();
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

    }
}
