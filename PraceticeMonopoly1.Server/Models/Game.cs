using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using CustomMonopoly.Server.Models.BoardSquares;

namespace CustomMonopoly.Server.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public int BoardId { get; set; }
        public ICollection<Player> Players { get; set; }
        [ForeignKey("BoardId")]
        public Board Board { get; set; }

    }
}
