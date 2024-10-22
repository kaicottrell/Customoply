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
       public List<RailRoadMappingSetting> RailRoadRentMappings { get; set; }
    }
}
