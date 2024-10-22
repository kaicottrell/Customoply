using CustomMonopoly.Server.Models;

namespace CustomMonopoly.Server
{
    public interface IPlayerEvent
    {
        Player Player { get; set; }
    }
}
