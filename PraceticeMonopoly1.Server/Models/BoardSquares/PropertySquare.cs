namespace CustomMonopoly.Server.Models.BoardSquares
{
    public abstract class PropertySquare : BoardSquare
    {
        public string Name { get; }
        public string Color { get; }
        public int MorgageValue { get; }
        public int Price { get; }
        public ICollection<PlayerProperty> PlayerProperties { get; set; }
        // Default constructor for EF Core
        protected PropertySquare() { }
        protected PropertySquare(string name, string color, int morgageValue, int price)
        {
            Name = name;
            Color = color;
            MorgageValue = morgageValue;
            Price = price;
            PlayerProperties = new List<PlayerProperty>();
        }

        //public void Deconstruct(out int propertyId, out int price)
        //{
        //    propertyId = Id;
        //    price = Price;
        //}
    }
    public class BuildablePropertySquare : PropertySquare
    {
        public int HouseHotelCost { get; }
        public int RentNoHouse { get; }
        public int RentOneHouse { get; }
        public int RentTwoHouse { get; }
        public int RentThreeHouse { get; }
        public int RentFourHouse { get; }
        public int RentHotel { get; }

        public BuildablePropertySquare(string name, string color, int morgageValue, int price, int rentNoHouse, int rentOneHouse, int rentTwoHouse, int rentThreeHouse, int rentFourHouse, int rentHotel, int houseHotelCost)
            : base(name, color, morgageValue, price)
        {
            HouseHotelCost = houseHotelCost;
        }
    }


}
