using CustomMonopoly.Server.Models.Settings;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomMonopoly.Server.Models.BoardSquares
{
    public class RailRoadSquare : PropertySquare
    {

        /// <summary>
        /// Stores the Mappings used for  
        /// </summary>
        [NotMapped]
        public List<RailRoadMappingSetting> RailRoadRentMappings { get; set; } = new List<RailRoadMappingSetting>();
        // Default constructor for EF Core
        public RailRoadSquare() { }

        // Parameterized constructor
        public RailRoadSquare(string name, string color, int morgageValue, int price)
            : base(name, color, morgageValue, price)
        {
        }
    }
}
