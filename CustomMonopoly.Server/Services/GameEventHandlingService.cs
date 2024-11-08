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
        public void HandleAvailableForPurchaseEvent(BoardEventDTO e, string? option)
        {
            if (e.AvailablePropertyDetailsDTO == null || e.Player == null)
            {
                throw new Exception("Expected PropertySqaure and Player to be non null, but found at least one null.");
            }
            if (option == null)
            {
                throw new Exception("Expected selected option, null value provided");
            }

            //Verify that the property is not already bought
            bool canBuy = _db.PlayerProperties.Any(p => p.PropertySquareId == e.AvailablePropertyDetailsDTO.PropertyId && p.Player.GameId == e.Player.GameId) == false;
            if (canBuy == false)
            {
                throw new Exception("Player Attempted to buy property that is already owned");
            }
            if (e.AvailablePropertyDetailsDTO.PropertyOptions == null || e.AvailablePropertyDetailsDTO.PropertyOptions.Contains(option) == false )
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
                    var player = _db.Players.Where(p => p.Id == e.Player.Id).First();
                    player.Balance -= e.AvailablePropertyDetailsDTO.PurchasePrice;
                    _db.Update(player);
                    //Assign the property to the player
                    PlayerProperty acquiredProperty = new PlayerProperty
                    {
                        PlayerId = e.Player.Id,
                        PropertySquareId = e.AvailablePropertyDetailsDTO.PropertyId
                    };
                    _db.Add(acquiredProperty);
                    _db.SaveChanges();
                    break;
                default:
                    throw new Exception("PropertyOption Not Implemented");
            }
        }
        public void HandleRentRequiredEvent(BoardEventDTO e)
        {
            if (e.RentDetailsDTO == null)
            {
                throw new Exception("Expected Rent Amount in the board event, but none listed.");
            }
            if (e.Player == null || e.RentDetailsDTO.ToPlayer == null)
            {
                throw new Exception("Expected Players to be non null, but found null");
            }
            //Take rent from player
            var fromPlayer = _db.Players.Where(p => p.Id == e.Player.Id).First();
            fromPlayer.Balance -= e.RentDetailsDTO.RentAmount;
            //Give balance to the TOPlayer
            var toPlayer = _db.Players.Where(p => p.Id == e.RentDetailsDTO.ToPlayer.Id).First();
            toPlayer.Balance += e.RentDetailsDTO.RentAmount;

            _db.Update(fromPlayer);
            _db.Update(toPlayer);
            _db.SaveChanges();
            //TODO: Handle Bankruptcy
        }

    }
}
