using CustomMonopoly.Server.Data;
using CustomMonopoly.Server.Models;
using CustomMonopoly.Server.ViewModels;
using CustomMonopoly.Server.ViewModels.DTOs;
using CustomMonopoly.Server.Config;
namespace CustomMonopoly.Server.Services
{
    public class GameEventHandlingService
    {
        private ApplicationDbContext _db;

        public GameEventHandlingService(ApplicationDbContext db)
        {
            _db = db;
        }
        public PlayerUpdateVM HandleAvailableForPurchaseEvent(BoardEventDTO e, string? option)
        {
            if (e.PropertyDTO == null || e.Player == null)
            {
                throw new Exception("Expected PropertySqaure and Player to be non null, but found at least one null.");
            }
            if (option == null)
            {
                throw new Exception("Expected selected option, null value provided");
            }

            //Verify that the property is not already bought
            bool canBuy = _db.PlayerProperties.Any(p => p.PropertySquareId == e.PropertyDTO.PropertyId && p.Player.GameId == e.Player.GameId) == false;
            if (canBuy == false)
            {
                throw new Exception("Player Attempted to buy property that is already owned");
            }
            if (e.PropertyOptions == null || e.PropertyOptions.Contains(option) == false )
            {
                throw new Exception("Option in response does not match the options available in the event");
            }
            // Handle the option selected
            switch (option)
            {
                //case PropertyOptionType.Auction:
                //    // TODO: Handle Auction
                //    break;
                case SD.Purchase:
                    e.Player.Balance -= e.PropertyDTO.Price;
                    _db.Update(e.Player);
                    //Assign the property to the player
                    PlayerProperty acquiredProperty = new PlayerProperty
                    {
                        PlayerId = e.Player.Id,
                        PropertySquareId = e.PropertyDTO.PropertyId
                    };
                    _db.Add(acquiredProperty);
                    _db.SaveChanges();
                   return new PlayerUpdateVM { Balance = e.Player.Balance, PlayerId = e.Player.Id };
                default:
                    throw new Exception("PropertyOption Not Implemented");
            }
        }
        public PlayerUpdateVM HandleRentRequiredEvent(BoardEventDTO e)
        {
            if (e.RentAmount == null)
            {
                throw new Exception("Expected Rent Amount in the board event, but none listed.");
            }
            if (e.Player == null || e.ToPlayer == null)
            {
                throw new Exception("Expected Players to be non null, but found null");
            }
            //Take rent from player
            e.Player.Balance -= e.RentAmount.Value;
            //Give balance to the TOPlayer
            e.ToPlayer.Balance += e.RentAmount.Value;
            _db.Update(e.Player);
            _db.Update(e.ToPlayer);
            _db.SaveChanges();
            return new PlayerUpdateVM { PlayerId = e.Player.Id, Balance = e.Player.Balance};

            //TODO: Handle Bankruptcy
        }

    }
}
