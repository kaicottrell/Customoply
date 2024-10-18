using System.ComponentModel.DataAnnotations.Schema;

namespace PraceticeMonopoly1.Server.Models.BoardSquares
{
    /// <summary>
    /// Acts as an associative table to bind board squares to boards
    /// </summary>
    public class BoardBoardSquare
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int BoardId { get; set; }
        public int BoardSquareId { get; set; }
        [ForeignKey("BoardId")]
        public virtual Board Board { get; set; }
        [ForeignKey("BoardSquareId")]
        public virtual BoardSquare BoardSquare { get; set; }

    }
}
