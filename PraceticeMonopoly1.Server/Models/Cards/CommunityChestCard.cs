﻿namespace PraceticeMonopoly1.Server.Models.Cards
{
    public class CommunityChestCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Contains the amount of money to win or lose
        /// </summary>
        public int MoneyDifference { get; set; }
        //TODO: Add move actions (moves a player, to a location)
        public int CreatorId { get; set; }
    }
}
