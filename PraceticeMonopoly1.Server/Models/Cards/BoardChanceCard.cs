namespace PraceticeMonopoly1.Server.Models.Cards
{
    /// <summary>
    /// Links the board to the created chance card
    /// </summary>
    public class BoardChanceCard
    {
        public int Id { get; set; }
        public int BoardId { get; set; }    
        public int ChanceCardId { get; set; }
    }
}
