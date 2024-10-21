using System.Runtime.ConstrainedExecution;

namespace CustomMonopoly.Server.Models.BoardSquares
{
    public class JailSquare : BoardSquare
    {
        public int TurnsInJail { get; } = 3; // Read-only property

    }
}
