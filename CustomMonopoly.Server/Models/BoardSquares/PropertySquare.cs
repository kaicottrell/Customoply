namespace CustomMonopoly.Server.Models.BoardSquares
{
    public abstract class PropertySquare : BoardSquare
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int MorgageValue { get; set; } 
        public int Price { get; set; }
        public ICollection<PlayerProperty> PlayerProperties { get; set; } = new List<PlayerProperty>();
        // Default constructor for EF Core
        public PropertySquare() { }
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
        public int HouseHotelCost { get; set; }
        public int RentNoHouse { get; set; }
        public int RentOneHouse { get; set; }
        public int RentTwoHouse { get; set; }
        public int RentThreeHouse { get; set; }
        public int RentFourHouse { get; set; }
        public int RentHotel { get; set; }
        public BuildablePropertySquare()
        {
            
        }
        public BuildablePropertySquare(string name, string color, int morgageValue, int price, int rentNoHouse, int rentOneHouse, int rentTwoHouse, int rentThreeHouse, int rentFourHouse, int rentHotel, int houseHotelCost)
            : base(name, color, morgageValue, price)
        {
            RentNoHouse = rentNoHouse;
            RentOneHouse = rentOneHouse;
            RentTwoHouse = rentTwoHouse;
            RentThreeHouse = rentThreeHouse;
            RentFourHouse = rentFourHouse;
            RentHotel = rentHotel;
            HouseHotelCost = houseHotelCost;
        }
    }


}
