namespace PraceticeMonopoly1.Server.Models
{
    /// <summary>
    /// Many to many table connecting players to owned properties
    /// </summary>
    public class PlayerProperty
    {
        [Key]
        public int PlayerId { get; set; }
        public int PropertyId { get; set; }
        public int HouseCount { get; set; } = 0;
        public int HotelCount { get; set; } = 0;
    }
}
