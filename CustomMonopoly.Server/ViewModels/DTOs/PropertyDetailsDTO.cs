using CustomMonopoly.Server.Models.BoardSquares;

namespace CustomMonopoly.Server.ViewModels.DTOs
{
    public class PropertyDetailsDTO
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public int PurchasePrice { get; set; }
        public int MorgageValue { get; set; }
        public string PropertyType { get; set; } 
        public string Color { get; set; }
        public int? HouseHotelCost { get; set; }
        public int? RentNoHouse { get; set; }
        public int? RentOneHouse { get; set; }
        public int? RentTwoHouse { get; set; }
        public int? RentThreeHouse { get; set; }
        public int? RentFourHouse { get; set; }
        public int? RentHotel { get; set; }
        public List<string>? PropertyOptions { get; set; }
        

        public PropertyDetailsDTO()
        {
            
        } 
        public PropertyDetailsDTO(PropertySquare property, List<string> propertyOptions )
        {
            PropertyId = property.Id;
            PropertyOptions = propertyOptions;
            Name = property.Name;
            PurchasePrice = property.Price;
            MorgageValue = property.MorgageValue;
            Color = property.Color;
            if (property is BuildablePropertySquare bps)
            {
                HouseHotelCost = bps.HouseHotelCost;
                RentNoHouse = bps.RentNoHouse;
                RentOneHouse = bps.RentOneHouse;
                RentTwoHouse = bps.RentTwoHouse;
                RentThreeHouse = bps.RentThreeHouse;
                RentFourHouse = bps.RentFourHouse;
                RentHotel = bps.RentHotel;  
            }

            PropertyType = property switch
            {
                BuildablePropertySquare => "Buildable",
                UtilitySquare => "Utility",
                RailRoadSquare => "RailRoad",
                _ => throw new Exception("Unexpected time of property square when converting to DTO")
            };
        }
    }
}
