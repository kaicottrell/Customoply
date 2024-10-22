using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CustomMonopoly.Server.Models.BoardSquares;

namespace CustomMonopoly.Server.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public int Balance { get; set; }
        public int? TurnsInJail { get; set; }
        public int CurrentPostion { get; set; }
        public int GameId { get; set; }
        public string UserId { get; set; } 
        /// <summary>
        /// Color of the players puck
        /// </summary>
        public string Color = "Blue";
        [ForeignKey("GameId")]
        public Game? Game { get; set; }

        public ICollection<PlayerProperty>? OwnedProperties { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        public Player(int balance, int? turnsInJail, int currentPosition, int gameId, string userId, string color)
        {
            Balance = balance;
            TurnsInJail = turnsInJail;
            CurrentPostion = currentPosition;
            GameId = gameId;
            UserId = userId;
            Color = color;
        }
        public int RollTwoDice()
        {
            Random rnd = new Random();
            return rnd.Next(0,7) + rnd.Next(0,7);
        }

    }
    
}
