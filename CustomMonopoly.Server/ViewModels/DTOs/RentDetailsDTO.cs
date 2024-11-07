namespace CustomMonopoly.Server.ViewModels.DTOs
{
    public class RentDetailsDTO
    {
        //RentRequired:
        public int RentAmount { get; set; }
        public PlayerDTO? ToPlayer { get; set; }
        public RentDetailsDTO()
        {
            
        }
        public RentDetailsDTO(int rentAmount, PlayerDTO toPlayer)
        {
            RentAmount = rentAmount;
            ToPlayer = toPlayer;
        }
    }
}
