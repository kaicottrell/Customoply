using CustomMonopoly.Server.Models;

namespace CustomMonopoly.Server.ViewModels
{
    public abstract class CardEvent : BoardEvent, IPlayerEvent
    {
        public CardType CardType { get; set; }
        public Player Player { get; set; }
    }

 

    public class PayMoneyCardEvent : CardEvent
    {
        /// <summary>
        /// Amount of money to be paid
        /// </summary>
        public decimal Amount { get; set; }
        public Player Player { get; set; }

    }

    public class GainMoneyCardEvent : CardEvent
    {
        /// <summary>
        /// Amount of money to be gained
        /// </summary>
        public decimal Amount { get; set; }
    }

    public class MoveToLocationCardEvent : CardEvent
    {
        /// <summary>
        /// Holds the location associated with the event type
        /// </summary>
        public int AssociatedLocationId { get; set; }
    }
    public enum CardType
    {
        Community, 
        Chance
    }
}

