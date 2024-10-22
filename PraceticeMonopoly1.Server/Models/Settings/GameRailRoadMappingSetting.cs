using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomMonopoly.Server.Models.Settings
{
    //Associative table for mapping games to railroadmapping settings
    public class GameRailRoadMappingSetting
    {
        [Key, Column(Order = 1)]
        public int GameId { get; set; }
        [Key, Column(Order = 2)]
        public int RailRoadMappingSettingId { get; set; }
        [ForeignKey("RailRoadMappingSettingId")]
        public RailRoadMappingSetting RailRoadMappingSetting { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
