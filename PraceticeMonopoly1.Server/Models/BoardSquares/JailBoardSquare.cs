using System.Runtime.ConstrainedExecution;

namespace PraceticeMonopoly1.Server.Models.BoardSquares
{
    public class JailBoardSquare : BoardSquare
    {
        public int TurnsInJail { get; } = 3; // Read-only property

    }
}
