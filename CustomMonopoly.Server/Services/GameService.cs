﻿using Microsoft.OpenApi.Services;
using CustomMonopoly.Server.Models;
using CustomMonopoly.Server.Models.BoardSquares;
using CustomMonopoly.Server.Data;
using CustomMonopoly.Server.ViewModels;
using CustomMonopoly.Server.Exceptions;
using Microsoft.EntityFrameworkCore;
using CustomMonopoly.Server.Config;
using CustomMonopoly.Server.Extensions;
using CustomMonopoly.Server.ViewModels.DTOs;

namespace CustomMonopoly.Server.Services
{
    /// <summary>
    /// Interacts directly with the database to process the handling of game
    /// specific  data such as configuring the game and the board, moving players, buying houses, etc.
    /// </summary>
    public class GameService
    {
        private readonly ApplicationDbContext _db;
        public GameService(ApplicationDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Rolls the players dice, and move the player along the board
        /// </summary>
        /// <param name="userId">The ApplicationUserid of the user making the action</param>
        /// <param name="gameId">The game being affected</param>
        /// <returns>A BoardEvent such as <see cref="AvailableForPurchaseEvent" /> or <see cref="GainMoneyCardEvent" /></returns>
        //TODO: Return a tuple of the GoEvent? find a way to display this on the frontend 
        public GameDTO MovePlayer(int gameId)
        {
            var game = GetExistingGame(gameId);

            var boardSquareList = Game.GetGameBoardSquareList(game);
            //Get the current player 
            var player = GetCurrentPlayer(gameId);
            // move the player to another spot on the board
            int diceRoll = player.RollTwoDice();
            int oldPlayerPosition = player.CurrentPostion;
            int newPlayerPosition = ((player.CurrentPostion + diceRoll) % boardSquareList.Count());
            //Lapped the board (reset)
            bool passedGo = oldPlayerPosition + diceRoll > boardSquareList.Count();
            if (passedGo)
            {
                player.Balance += 200;
            }
            player.CurrentPostion = newPlayerPosition;
            _db.Update(player);
            _db.SaveChanges();
            // Get the boardsquare at the new postion and return type
            //Return the property and if it is available for purchase
            BoardSquare boardSquare = boardSquareList[player.CurrentPostion];
            // We need to be able to handle a wide variety of events,  such as returning views for a card draw,
            // or property options such as pay rent or buy if available, or go to jail or free parking
            BoardEventDTO boardEventDTO = new BoardEventDTO();

            switch (boardSquare)
            {
                case PropertySquare propertySquare:
                    // See if PlayerProperty exists
                    var playerProperty = _db.PlayerProperties.Where(pp => pp.Player.GameId == gameId && pp.PropertySquareId == boardSquare.Id)
                        .Include(pp => pp.Player)
                        .FirstOrDefault();

                    // Based on if the property is owned we return an event
                    if (playerProperty == null)
                    {
                        //Determine if player has enough cash
                        List<string> propertyOptions = new List<string>() { SD.Auction };
                        if (player.Balance > propertySquare.Price)
                        {
                            propertyOptions.Add(SD.Purchase);
                        }
                        // return the available for purchase event
                        boardEventDTO = new BoardEventDTO(propertySquare, propertyOptions, player);
                    }
                    // Owned by the player who landed on the property square
                    else if (playerProperty.PlayerId == player.Id)
                    {
                        boardEventDTO = new BoardEventDTO
                        {
                            Description = "Home Sweet Home",
                            EventType = SD.HomeNoActionBoardEvent
                        };
                    }
                    //Owned by another player -- pay rent
                    else
                    {
                        int rent = DetermineRent(playerProperty, propertySquare, diceRoll);
                        var toPlayer = playerProperty.Player;
                        boardEventDTO = new BoardEventDTO(propertySquare, player, toPlayer, rent);
                    }
                    break;
                //TODO: Implement the Card Service to handle drawing a random chance or community card 
                //case ChanceSquare chanceSquare:
                //    // Handle chance square logic here
                //    return new CardEvent { CardType = CardType.Chance }; 

                //case CommunityChestSquare communityChestSquare:
                //    // Handle community chest square logic here
                //    break;

                //case FreeParkingSquare freeParkingSquare:
                //    break;
                //case GoToJailSquare goToJailSquare:
                //    break;
                //case JailSquare jailSquare:
                //    break;
                default:
                    boardEventDTO = new BoardEventDTO
                    {
                        Description = "Home Sweet Home",
                        EventType = SD.HomeNoActionBoardEvent
                    };
                    break;
            }

            boardEventDTO.PlayerPassedGo = passedGo;
            game = GetExistingGame(gameId);

            if (game == null)
            {
                throw new Exception("Game no longer exists");
            }
            var gameDTO = game.ToGameDTO();
            gameDTO.CurrentBoardEvent = boardEventDTO;
            
            return gameDTO;
        }
        /// <summary>
        /// Saves the game, Sets the board
        /// </summary>
        /// <param name="game"></param>
        /// <returns>the game id of the newly created game</returns>
        public Game StartGame()
        {
            Game game = new Game();
            var gameBoard = _db.Boards
                .Include(b => b.BoardBoardSquares)
                    .ThenInclude(bbs => bbs.BoardSquare)
                .First();

            game.Board = gameBoard;
            _db.Add(game);
            _db.SaveChanges();
            return game;
        }
        public List<Player> ConfigurePlayersForGame(Game newGame, string userId, int numPlayers)
        {
            if (numPlayers > PlayerConfig.PlayerColors.Length)
            {
                throw new Exception("Unsupported amount of players when configuring the game.");
            }

            var firstPlayer = new Player(1500, null, 0, newGame.Id, userId, PlayerConfig.PlayerColors[0], true, 0);
            List<Player> playerList = new List<Player>() { firstPlayer };
            _db.Add(firstPlayer);
            for (int i = 1; i < numPlayers; i++)
            {
                //Add any additional players
                var additionalPlayer = new Player(1500, null, 0, newGame.Id, userId, PlayerConfig.PlayerColors[i], false, i);
                playerList.Add(additionalPlayer);
                _db.Add(additionalPlayer);
            }

            _db.SaveChanges();
            return playerList;
        }
        public Game? GetExistingGame(string userId)
        {
            var existingGame = _db.Games
                .Include(g => g.Players)
                .Include(g => g.Board)
                    .ThenInclude(b => b.BoardBoardSquares)
                    .ThenInclude(bbs => bbs.BoardSquare)
                .Where(g => g.Players.Any(p => p.UserId == userId))
                .FirstOrDefault();
            return existingGame;
        }
        public Game? GetExistingGame(int gameId)
        {
            var existingGame = _db.Games
                .Include(g => g.Players)
                .Include(g => g.Board)
                    .ThenInclude(b => b.BoardBoardSquares)
                    .ThenInclude(bbs => bbs.BoardSquare)
                .Where(g => g.Id == gameId)
                .FirstOrDefault();
            return existingGame;
        }
        /// <summary>
        /// Determines the rent cost based the type of property, monopolies, and on the number of houses and if there is a hotel
        /// </summary>
        /// <returns>An integer value representing the rent owed for landing on a property</returns>
        private int DetermineRent(PlayerProperty playerProperty, PropertySquare propertySquare, int diceRoll)
        {
            //Determine if it is a utlity, train, or buildable property and act accordingly
            switch (propertySquare)
            {
                case BuildablePropertySquare buildablePropertySquare:
                    return CalculateBuildablePropertyRent(buildablePropertySquare, playerProperty);
                case RailRoadSquare railRoadSquare:
                    return CalculateRailroadRent(playerProperty);
                case UtilitySquare utilitySquare:
                    return CalculateUtilityPropertyRent(utilitySquare, playerProperty, diceRoll);
                default:
                    throw new NotImplementedException();
            }

        }
        /// <summary>
        /// Returns the Property Squares associated with the given player
        /// </summary>
        /// <param name="playerId">The identifier for the associated player</param>
        /// <returns>List of properties owned by the player</returns>
        public async Task<List<PropertySquare>> GetPlayerPropertySquares(int playerId)
        {
            List<PropertySquare> playerProperties = await _db.PlayerProperties.Where(pp => pp.PlayerId == playerId)
                .Include(pp => pp.PropertySquare)
                .Select(pp => pp.PropertySquare)
                .ToListAsync();

            return playerProperties;
        }
        private int CalculateRailroadRent(PlayerProperty playerProperty)
        {
            // rent is based on the number of railroads as defined by the railroad settings.
            var ownedByPlayer = playerProperty.Player;

            //TODO: Create own generic repository to handle includes in a more efficient way
            var associatedRailroadPlayerProperties = _db.PlayerProperties.Where(pp => pp.PropertySquare is RailRoadSquare && pp.PlayerId == ownedByPlayer.Id)
                .Include("PropertySquare")
                .ToList();
            //utilize the railRoad game settings if any otherwise use default. 
            var railroadGameSettings = _db.GameRailRoadMappingSettings
                .Where(grrms => grrms.GameId == ownedByPlayer.GameId)
                .Include("RailRoadMappingSetting")
                .ToList();

            int ownedRailroadCount = associatedRailroadPlayerProperties.Count();
            if (railroadGameSettings.Any())
            {
                //get the associated amount from the settings
                var associatedRailroadSetting = railroadGameSettings.Where(ars => ars.RailRoadMappingSetting.NumberOfRailRoadsOwned == ownedRailroadCount)
                    .FirstOrDefault();

                if (associatedRailroadSetting == null)
                    throw new InvalidOperationException($"Setting not found for the current number of railroads as owned by player: {ownedByPlayer.Id}");
                return associatedRailroadSetting.RailRoadMappingSetting.RentCost;
            }

            // Default
            return ownedRailroadCount switch
            {
                1 => 25,
                2 => 50,
                3 => 100,
                4 => 200,
                _ => throw new InvalidOperationException($"Invalid Number of RailRoads owned by player: {ownedByPlayer.Id}")
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game">Game with associated players</param>
        public void UpdateToNextPlayerTurn(int gameId)
        {
            // get the game and current player
            var game = GetExistingGame(gameId);
            var currentPlayer = GetCurrentPlayer(gameId);
            // based on the order of the player turns 
            int playerCount = game.Players.Count;
            currentPlayer.IsPlayersTurn = false;

            int newPlayersTurnOrder = (currentPlayer.TurnOrder + 1) % playerCount;
            //Set the next players turn to true
            var nextPlayer = _db.Players.Where(p => p.GameId == game.Id && p.TurnOrder == newPlayersTurnOrder).First();
            nextPlayer.IsPlayersTurn = true;

            _db.Update(currentPlayer);
            _db.Update(nextPlayer);
            _db.SaveChanges();
        }
        private Player GetCurrentPlayer(int gameId)
        {
            return _db.Players.Where(p => p.GameId == gameId && p.IsPlayersTurn).First();
        }
        private int CalculateBuildablePropertyRent(BuildablePropertySquare buildablePropertySquare, PlayerProperty playerProperty)
        {
            // rent is based on the number of railroads as defined by the railroad settings.
            var ownedByPlayer = playerProperty.Player;

            // See if it is a monopoly which doubles rent
            // Get all of the player properties of the same color owned by the player
            var ownedSameColorPropertyCount = _db.PlayerProperties.Count(pp =>
                pp.PropertySquare is BuildablePropertySquare &&
                pp.PropertySquare.Color == buildablePropertySquare.Color &&
                pp.PlayerId == ownedByPlayer.Id
            );

            var gameSameColorPropertyCount = _db.Games
               .Where(game => game.Id == ownedByPlayer.GameId)
               .Include(game => game.Board)
                .ThenInclude(board => board.BoardBoardSquares)
                    .ThenInclude(bbs => bbs.BoardSquare)
                .SelectMany(game => game.Board.BoardBoardSquares)
                .Count(bs => bs.BoardSquare is BuildablePropertySquare && ((BuildablePropertySquare)bs.BoardSquare).Color == buildablePropertySquare.Color);

            bool isMonopoly = ownedSameColorPropertyCount == gameSameColorPropertyCount;
            int multiplier = (isMonopoly ? 2 : 1);

            if (playerProperty.HotelCount == 1)
            {
                return buildablePropertySquare.RentHotel;
            }

            return playerProperty.HouseCount switch
            {
                0 => buildablePropertySquare.RentNoHouse * multiplier,
                1 => buildablePropertySquare.RentOneHouse,
                2 => buildablePropertySquare.RentTwoHouse,
                3 => buildablePropertySquare.RentThreeHouse,
                4 => buildablePropertySquare.RentFourHouse,
                _ => throw new InvalidPropertyHouseCountException()
            };


        }
        private int CalculateUtilityPropertyRent(UtilitySquare utilitySquare, PlayerProperty playerProperty, int diceRoll)
        {
            var ownedByPlayer = playerProperty.Player;

            var associatedUtilityPlayerProperties = _db.PlayerProperties.Where(pp => pp.PropertySquare is UtilitySquare && pp.PlayerId == ownedByPlayer.Id)
                    .Include("PropertySquare")
                    .ToList();
            ///todo: add customizablility here to allow for games to have utility settings similar to <see cref="GameRailRoadMappingSetting">
            int countOfUtilProperties = associatedUtilityPlayerProperties.Count();

            return countOfUtilProperties switch
            {
                1 => utilitySquare.BaseRent * diceRoll,
                2 => (int)(utilitySquare.BaseRent * diceRoll * 2.5),
                _ => throw new InvalidOperationException("This number of utility properties is not expected to be owned by the player")
            };
        }
        //public List<BoardSquare> GetGameBoard()
        //{
        //}


    }
}
