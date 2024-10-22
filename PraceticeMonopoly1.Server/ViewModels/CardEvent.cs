namespace CustomMonopoly.Server.ViewModels
{
    public abstract class CardEvent : BoardEvent
    {
        public CardType CardType { get; set; }
    }

 

    public class PayMoneyCardEvent : CardEvent
    {
        /// <summary>
        /// Holds the player associated with the event type
        /// </summary>
        public int AssociatedPlayerId { get; set; }

        /// <summary>
        /// Amount of money to be paid
        /// </summary>
        public decimal Amount { get; set; }
    }

    public class GainMoneyCardEvent : CardEvent
    {

        /// <summary>
        /// Holds the player associated with the event type
        /// </summary>
        public int AssociatedPlayerId { get; set; }

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

