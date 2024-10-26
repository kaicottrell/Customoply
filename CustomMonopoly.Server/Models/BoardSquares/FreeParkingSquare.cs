namespace CustomMonopoly.Server.Models.BoardSquares
{
    public class FreeParkingSquare : BoardSquare
    {
        public int CashPrize { get; set; }
        public FreeParkingSquare()
        {
            CashPrize = 0;
        }
        public FreeParkingSquare(int cashPrize)
        {
            CashPrize = cashPrize;
        }
    }
}
