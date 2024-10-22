namespace CustomMonopoly.Server.Models.BoardSquares
{
    public class UtilitySquare : PropertySquare
    {
        /// <summary>
        /// BaseRent to then be multiplied by the dice roll
        /// </summary>
        public int BaseRent { get; } 
        public UtilitySquare(string name, string color, int morgageValue, int price, int baseRent)  : base(name, color, morgageValue, price)
        {
            BaseRent = baseRent;
        }
    }
}
