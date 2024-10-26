namespace CustomMonopoly.Server.Models.DTOs
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public int Balance {  get; set; }
        public int CurrentPosition { get; set; }
        public string Color { get; set; }
        public PlayerDTO(Player player)
        {
            Id = player.Id;
            Balance = player.Balance;
            CurrentPosition = player.CurrentPostion;
            Color = player.Color;
        }
    }
}
