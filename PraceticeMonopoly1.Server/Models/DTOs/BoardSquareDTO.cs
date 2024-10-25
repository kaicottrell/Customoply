using Azure.Identity;
using CustomMonopoly.Server.Models.BoardSquares;
namespace CustomMonopoly.Server.Models.DTOs
{
    public class BoardSquareDTO
    {
        public string? Color { get; set; } 
        public int OrderNumber { get; set; }
        public string Type { get; set; }
        public int? HouseCount { get; set; }
        public bool? HasHotel { get; set; }
        public BoardSquareDTO(BoardSquare boardSquare, int order)
        {
            OrderNumber = order;
            Color = boardSquare is PropertySquare ps ? ps.Color : null;
            Type = boardSquare switch
            {
                BuildablePropertySquare => BoardSquareType.BuildableProperty.ToString(),
                RailRoadSquare => BoardSquareType.RailroadProperty.ToString(),
                ChanceSquare => BoardSquareType.Chance.ToString(),
                CommunityChestSquare => BoardSquareType.CommunityChest.ToString(),
                GoToJailSquare => BoardSquareType.GoToJail.ToString(),
                JailSquare => BoardSquareType.Jail.ToString(),
                FreeParkingSquare => BoardSquareType.FreeParking.ToString(),
                GoSquare => BoardSquareType.Go.ToString(),
                UtilitySquare => BoardSquareType.Utility.ToString(),
                TaxSquare => BoardSquareType.Tax.ToString(),
                _ => throw new ArgumentOutOfRangeException(nameof(boardSquare), $"Unhandled board square type: {boardSquare.GetType()}")
            };
            //TODO: Implement house count if the type is a buildable property 
        }
        public enum BoardSquareType
        {
            BuildableProperty,
            RailroadProperty,
            Chance,
            CommunityChest,
            GoToJail,
            Jail,
            FreeParking,
            Go,
            Utility,
            Tax
        }

    }
   
}
