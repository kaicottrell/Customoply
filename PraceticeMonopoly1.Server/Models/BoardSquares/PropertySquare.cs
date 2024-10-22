namespace CustomMonopoly.Server.Models.BoardSquares
{
    public class PropertySquare : BoardSquare
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int RentNoHouse { get; set; }
        public int RentOneHouse { get; set; }
        public int RentTwoHouse { get; set; }
        public int RentThreeHouse { get; set; }
        public int RentFourHouse { get; set; }
        public int RentHotel { get; set; }
        public int MorgageValue { get; set; }
        public int Price { get; set; }
        public ICollection<PlayerProperty> PlayerProperties { get; set; }
    }
}
