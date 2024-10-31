
namespace CustomMonopoly.Server.ViewModels
{
    public class BoardEventResponse
    {
        public int GameId { get; set; }
        public BoardEvent BoardEvent { get; set; }
    }
    public class AvailableForPurchaseEventResponse : BoardEventResponse
    {
        public PropertyOptionType PropertyOptionType { get; set; }
    }
}
