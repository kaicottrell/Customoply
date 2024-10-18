using System.ComponentModel.DataAnnotations.Schema;
using PraceticeMonopoly1.Server.Models.BoardSquares;

namespace PraceticeMonopoly1.Server.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public int Balance { get; set; }
        public int? TurnsInJail { get; set; }
        public int CurrentPostion { get; set; }
        public int GameId { get; set; }
        public int NumberOfRailroads { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }

        ICollection<PropertySquare> OwnedProperties { get; set; }

        public int Roll2Dice()
        {
            Random rnd = new Random();
            return rnd.Next(0,7) + rnd.Next(0,7);
        }

    }
    
}
