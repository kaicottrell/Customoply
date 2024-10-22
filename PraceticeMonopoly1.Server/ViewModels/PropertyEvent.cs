﻿using CustomMonopoly.Server.Models.BoardSquares;
using System.ComponentModel.DataAnnotations;

namespace CustomMonopoly.Server.ViewModels
{
  
    public abstract class PropertyEvent : BoardEvent
    {
        // Common properties for all property events can be added here
        public PropertySquare PropertySquare { get; protected set; }
    }

    public class AvailableForPurchaseEvent : PropertyEvent
    {
        public List<PropertyOptionType> PropertyOptions { get; set; }
        public int PurchasePrice { get; set; }
        public AvailableForPurchaseEvent(int purchasePrice)
        {
            Description = "Available for Purchase";
            PropertyOptions = new List<PropertyOptionType> { PropertyOptionType.Purchase, PropertyOptionType.Auction };
            PurchasePrice = purchasePrice;
        }
        public AvailableForPurchaseEvent(int purchasePrice, List<PropertyOptionType> propertyOptions)
        {
            PropertyOptions = propertyOptions;
            PurchasePrice = purchasePrice;
            if (propertyOptions.Contains(PropertyOptionType.Purchase))
            {
                Description = "Available for Purchase or Auction";
            }
            else
            {
                Description = "INSUFFICIENT FUNDS: Auction";
            }
        }

        public enum PropertyOptionType
        {
            Purchase,
            Auction
        }
    }
 

    public class HomeNoActionEvent : PropertyEvent
    {
        public HomeNoActionEvent()
        {
            Description = "Home Sweet Home" ;
        }
    }

    public class RentRequiredEvent : PropertyEvent
    {
        public int RentAmount { get; set; }
        public string PlayerColor { get; set; }
        public RentRequiredEvent(int rentAmount, string payPlayerMessage, string playerColor)
        {
            RentAmount = rentAmount;
            Description = payPlayerMessage;
            PlayerColor = playerColor;
        }

    }
}

