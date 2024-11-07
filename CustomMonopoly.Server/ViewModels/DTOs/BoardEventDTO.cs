using CustomMonopoly.Server.Models.BoardSquares;
using CustomMonopoly.Server.Models;
using System.Text.Json.Serialization;
using CustomMonopoly.Server.Config;
using System.Numerics;
using CustomMonopoly.Server.Extensions;
namespace CustomMonopoly.Server.ViewModels.DTOs
{
    //TODO: Save in the database:
    // - This will make it so that the game can be played and manage a current board event (each game has one (or none) current board events)
    // - This will take less of the work on the frontend requiring the board event to be passed back to the backend
    public class BoardEventDTO
    {
        /// <summary>
        /// Description holds the event information to display to the user
        /// </summary>
        public string Description { get; set; }
        public string EventType { get; set; }
        public bool PlayerPassedGo { get; set; } = false;

        // Common properties for all property events can be added here
        public PropertyDetailsDTO? PropertyDetailsDTO { get;  set; }
        /// <summary>s
        /// Holds the player associated with the event type
        /// </summary>
        public PlayerDTO? Player { get; set; }
        public RentDetailsDTO? RentDetailsDTO { get; set; }
        //Default constructor for json 
        public BoardEventDTO() {}
        /// <summary>
        /// Used for initializing a buildable property event for auction or for purchase
        /// </summary>
        /// <param name="propertySquare"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="player"></param>
        public BoardEventDTO(PropertySquare propertySquare, List<string> propertyOptions, Player player)
        {
            Description = "Buy or Purchase";
            EventType = SD.PurchaseOrAuctionPropertyBoardEvent;
            PropertyDetailsDTO = new PropertyDetailsDTO(propertySquare, propertyOptions);
            Player = player.ToPlayerDTO();
        }
        /// <summary>
        /// Used for initializing a rent required event with the appropriate rent required details
        /// </summary>
        /// <param name="propertySquare"></param>
        /// <param name="propertyOptions"></param>
        /// <param name="player"></param>
        public BoardEventDTO(PropertySquare propertySquare, Player currentPlayer, Player paidToPlayer, int rent)
        {
            Description = $"Pay {paidToPlayer.Color} Player ${rent} for {propertySquare.Name}";
            EventType = SD.RentRequiredBoardEvent;
            Player = currentPlayer.ToPlayerDTO();
            RentDetailsDTO = new RentDetailsDTO(rent, paidToPlayer.ToPlayerDTO());
        }


    }
  
}
