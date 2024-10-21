using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomMonopoly.Server.Models.BoardSquares
{
    /// <summary>
    /// Acts as an associative table to bind board squares to boards
    /// </summary>
    public class BoardBoardSquare
    {
        [Key, Column(Order = 1)]
        public int BoardId { get; set; }
        [Key, Column(Order = 2)]
        public int BoardSquareId { get; set; }
        public int Order { get; set; }
        [ForeignKey("BoardId")]
        public virtual Board Board { get; set; }
        [ForeignKey("BoardSquareId")]
        public virtual BoardSquare BoardSquare { get; set; }

    }
}
