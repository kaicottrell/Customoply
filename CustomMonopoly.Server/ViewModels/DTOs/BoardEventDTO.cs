using CustomMonopoly.Server.Models.BoardSquares;
using CustomMonopoly.Server.Models;
using System.Text.Json.Serialization;

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
        public PropertyDTO? PropertyDTO { get;  set; }
        /// <summary>
        /// Holds the player associated with the event type
        /// </summary>
        public PlayerDTO? Player { get; set; }
        //PurchaseAuctionEvent
        public List<string>? PropertyOptions { get; set; }
        public int? PurchasePrice { get; set; }

        //RentRequired:
        public int? RentAmount { get; set; }
        public PlayerDTO? ToPlayer { get; set; }
        public BoardEventDTO()
        {
            
        }
    }
  
}
