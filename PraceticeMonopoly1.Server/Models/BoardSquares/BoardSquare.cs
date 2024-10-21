using System.ComponentModel.DataAnnotations;

namespace CustomMonopoly.Server.Models.BoardSquares
{
    public abstract class BoardSquare 
    {
        [Key]
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public ICollection<BoardBoardSquare> BoardBoardSquares { get; set; }

    }
}
