namespace CustomMonopoly.Server.Models.BoardSquares
{
    public class TaxSquare : BoardSquare
    {
        public int TaxCost { get; set; }
        public TaxSquare(int taxCost)
        {
            TaxCost = taxCost;
        }
    }
}
