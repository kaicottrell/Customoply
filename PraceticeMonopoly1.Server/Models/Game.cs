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
        public ICollection<Player> Players { get; set; }
        public List<BoardSquare> Board { get; set; }

      
        public Game() {
            
        }



    }
    public enum Outcome {
        PayRent,
        BuyProperty
    }

}
