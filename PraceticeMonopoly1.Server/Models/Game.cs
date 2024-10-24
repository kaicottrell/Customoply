using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using CustomMonopoly.Server.Models.BoardSquares;

namespace CustomMonopoly.Server.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public int BoardId { get; set; }
        public ICollection<Player> Players { get; set; }
        [ForeignKey("BoardId")]
        public Board Board { get; set; }

        /// <summary>
        /// Returns a list of board squares associated with the game board. Requires that board > boardboardsquares have value
        /// </summary>
        /// <returns></returns>
        public static List<BoardSquare> GetGameBoardSquareList(Game game)
        {
            if(game.Board == null || game.Board.BoardBoardSquares.Count() == 0)
            {
                throw new Exception("Inappropriate use of function, ensure that board.boardboardsquares are correctly joined through LINQ");
            }
            return game.Board.BoardBoardSquares
                .OrderBy(bbs => bbs.Order )
                .Select(bbs => bbs.BoardSquare)
                .ToList();
        }

    }
}
