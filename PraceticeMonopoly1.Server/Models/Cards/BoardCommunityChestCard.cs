using CustomMonopoly.Server.Models.BoardSquares;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomMonopoly.Server.Models.Cards
{
    public class BoardCommunityChestCard
    {
        [Key, Column(Order = 1)]
        public int BoardId { get; set; }

        [Key, Column(Order = 2)]
        public int CommunityChestCardId { get; set; }

        [ForeignKey("BoardId")]
        public Board Board { get; set; }

        [ForeignKey("CommunityChestCardId")]
        public CommunityChestCard CommunityChestCard { get; set; }
    }
}
