using CustomMonopoly.Server.Models.BoardSquares;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomMonopoly.Server.Models.Cards
{
    /// <summary>
    /// Links the board to the created chance card
    /// </summary>
    public class BoardChanceCard
    {
        [Key, Column(Order = 1)]
        public int BoardId { get; set; }
        [Key, Column(Order = 2)]
        public int ChanceCardId { get; set; }
        [ForeignKey("BoardId")]
        public Board Board { get; set; } 
        [ForeignKey("ChanceCardId")]
        public ChanceCard ChanceCard { get; set; }

    }
}
