using CustomMonopoly.Server.Models.BoardSquares;
using CustomMonopoly.Server.ViewModels;

namespace CustomMonopoly.Server.Models.DTOs
{
    /// <summary>
    /// Contains the data necessary to process the frontend for a game
    /// </summary>
    public class GameDTO
    {
        public int Id { get; set; }
        public List<PlayerDTO> PlayerList { get; set; } = new List<PlayerDTO>();
        public List<BoardSquareDTO> BoardSquares { get; set; } = new List<BoardSquareDTO>();
        public string BoardName { get; set; } = string.Empty;
        public BoardEvent? CurrentBoardEvent { get; set; }
    }
}
