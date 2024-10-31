using CustomMonopoly.Server.Models.DTOs;
using System.Text.Json.Serialization;

namespace CustomMonopoly.Server.ViewModels
{
    public abstract class BoardEvent
    {
        /// <summary>
        /// Description holds the event information to display to the user
        /// </summary>
        public string Description { get; set; }
        public string EventType { get; set; }
    }
}
