using CustomMonopoly.Server.Models;
using CustomMonopoly.Server.ViewModels.DTOs;

namespace CustomMonopoly.Server.Extensions
{
    public static class GameExtensions
    {
        public static GameDTO ToGameDTO(this Game game)
        {   
            return new GameDTO
            {
                Id = game.Id,
                PlayerList = game.Players.Select(p => new PlayerDTO(p)).OrderBy(p => p.Id).ToList(),
                BoardSquares = game.Board.BoardBoardSquares.Select(bbs => new BoardSquareDTO(bbs.BoardSquare, bbs.Order))
                    .OrderBy(bsdto => bsdto.OrderNumber)
                    .ToList(),
                BoardName = game.Board.Name
            };
        }
        public static PlayerDTO ToPlayerDTO(this Player player)
        {
            return new PlayerDTO(player);
        }

    }
}
