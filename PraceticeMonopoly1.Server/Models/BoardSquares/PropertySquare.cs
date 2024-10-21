namespace CustomMonopoly.Server.Models.BoardSquares
{
    public class PropertySquare : BoardSquare
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int Rent { get; set; }
        public int MorgageValue { get; set; }
        public int Cost { get; set; }
        public bool IsRailRoad { get; set; }
        public ICollection<PlayerProperty> PlayerProperties { get; set; }
    }
}
