using CustomMonopoly.Server.ViewModels.DTOs;

namespace CustomMonopoly.Server.ViewModels.DTOs.Responses
{
    public class BoardEventResponse
    {
        public int GameId { get; set; }
        public BoardEventDTO BoardEvent { get; set; }
        public string? SelectedPropertyOption { get; set; }
    }
}
