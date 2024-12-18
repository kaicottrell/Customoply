﻿using CustomMonopoly.Server.ViewModels.DTOs;
using System.Runtime.CompilerServices;
using CustomMonopoly.Server.Models.BoardSquares;
namespace CustomMonopoly.Server.Extensions
{
    public static class PropertyExtensions
    {
        public static PropertyDetailsDTO ToPropertyDTO(this PropertySquare propertySquare)
        {
            var newPropertyDTO = new PropertyDetailsDTO
            {   
                PropertyId = propertySquare.Id,
                Name = propertySquare.Name,
                PurchasePrice = propertySquare.Price,
                MorgageValue = propertySquare.MorgageValue,
                Color = propertySquare.Color
            };

            if (propertySquare is BuildablePropertySquare bps)
            {
                newPropertyDTO.HouseHotelCost = bps.HouseHotelCost;
                newPropertyDTO.RentOneHouse = bps.RentOneHouse;
                newPropertyDTO.RentNoHouse = bps.RentNoHouse;
                newPropertyDTO.RentTwoHouse = bps.RentTwoHouse;
                newPropertyDTO.RentThreeHouse = bps.RentThreeHouse;
                newPropertyDTO.RentFourHouse = bps.RentFourHouse;
                newPropertyDTO.RentHotel = bps.RentHotel;
            }

            newPropertyDTO.PropertyType = propertySquare switch
            {
                BuildablePropertySquare => "Buildable",
                UtilitySquare => "Utility",
                RailRoadSquare => "RailRoad",
                _ => throw new Exception("Unexpected time of property square when converting to DTO")
            };


            return newPropertyDTO;
        }
    }
}
