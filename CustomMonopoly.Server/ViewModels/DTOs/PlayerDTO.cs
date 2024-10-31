using CustomMonopoly.Server.Models;

namespace CustomMonopoly.Server.ViewModels.DTOs
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int Balance { get; set; }
        public int CurrentPosition { get; set; }
        public string Color { get; set; }
        public bool IsPlayersTurn { get; set; }
        public PlayerDTO(Player player)
        {
            Id = player.Id;
            Balance = player.Balance;
            CurrentPosition = player.CurrentPostion;
            Color = player.Color;
            IsPlayersTurn = player.IsPlayersTurn;
            GameId = player.GameId;
        }
    }
}
