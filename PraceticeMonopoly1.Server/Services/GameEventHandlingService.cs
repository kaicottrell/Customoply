using CustomMonopoly.Server.Data;
using CustomMonopoly.Server.Models;
using CustomMonopoly.Server.ViewModels;

namespace CustomMonopoly.Server.Services
{
    public class GameEventHandlingService
    {
        private ApplicationDbContext _db;

        public GameEventHandlingService(ApplicationDbContext db)
        {
            _db = db;
        }
        public PlayerUpdateVM HandleAvailableForPurchaseEvent(AvailableForPurchaseEvent e, PropertyOptionType option)
        {
            //Verify that the property is not already bought
            bool canBuy = _db.PlayerProperties.Any(p => p.PropertySquareId == e.PropertySquare.Id && p.Player.Game.Id == e.Player.GameId) == false;
            if (canBuy == false)
            {
                throw new Exception("Player Attempted to buy property that is already owned");
            }
            if (e.PropertyOptions.Contains(option) == false )
            {
                throw new Exception("Option in response does not match the options available in the event");
            }
            // Handle the option selected
            switch (option)
            {
                //case PropertyOptionType.Auction:
                //    // TODO: Handle Auction
                //    break;
                case PropertyOptionType.Purchase:
                    e.Player.Balance -= e.PropertySquare.Price;
                    _db.Update(e.Player);
                    //Assign the property to the player
                    PlayerProperty acquiredProperty = new PlayerProperty
                    {
                        PlayerId = e.Player.Id,
                        PropertySquareId = e.PropertySquare.Id
                    };
                    _db.Add(acquiredProperty);
                    _db.SaveChanges();
                   return new PlayerUpdateVM { Balance = e.Player.Balance, PlayerId = e.Player.Id };
                default:
                    throw new Exception("PropertyOption Not Implemented");
            }
        }
        public PlayerUpdateVM HandleRentRequiredEvent(RentRequiredEvent e)
        {
            e.Player.Balance -= e.RentAmount;
            _db.Add(e.Player);
            _db.SaveChanges();
            return new PlayerUpdateVM { PlayerId = e.Player.Id, Balance = e.Player.Balance};

            //TODO: Handle Bankruptcy
        }

    }
}
