namespace PraceticeMonopoly1.Server.Models.BoardSquares
{
    public abstract class BoardSquare 
    {
        [Key]
        public int Id { get; set; }
        public int CreatorId { get; set; }

    }
}
