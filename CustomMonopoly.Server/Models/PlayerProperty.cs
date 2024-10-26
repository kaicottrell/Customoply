using CustomMonopoly.Server.Models.BoardSquares;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomMonopoly.Server.Models
{
    /// <summary>
    /// Many to many table connecting players to owned properties
    /// </summary>
    public class PlayerProperty
    {
        [Key, Column(Order = 1)]
        public int PlayerId { get; set; }
        [Key, Column(Order = 2)]
        public int PropertySquareId { get; set; }
        public int? HouseCount { get; set; }
        public int? HotelCount { get; set; } 

        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
        [ForeignKey("PropertySquareId")]
        public PropertySquare PropertySquare { get; set; }
    }
}
