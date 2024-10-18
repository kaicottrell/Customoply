using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using PraceticeMonopoly1.Server.Models.BoardSquares;

namespace PraceticeMonopoly1.Server.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public ICollection<Player> Players { get; set; }
        public List<BoardSquare> Board { get; set; }

      
        public Game() {
            ConfigureGame();
        }

        //Sets up the board spaces
        public void ConfigureGame()
        {
            
        }


    }
    public enum Outcome {
        PayRent,
        BuyProperty
    }

}
