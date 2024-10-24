using CustomMonopoly.Server.Data;
using CustomMonopoly.Server.Models;
using CustomMonopoly.Server.Models.BoardSquares;
using Microsoft.EntityFrameworkCore;

namespace CustomMonopoly.Server.Services
{
    public class DatabaseInitializer
    {
        private readonly ApplicationDbContext _context;

        public DatabaseInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            if (!_context.BoardSquares.Any())
            {
                var defaultBoardSquares = ConfigureDefaultBoardSquares();
                _context.BoardSquares.AddRange(defaultBoardSquares);
                _context.SaveChanges();
                //Create a default board
               
                var defaultBoard = CreateDefaultBoard();
                _context.Boards.Add(defaultBoard);
                _context.SaveChanges();
                //Hook up all of the default board squares with the default board

                var defaultBoardBoardSquares = LinkBoardSquaresToBoard(defaultBoardSquares, defaultBoard);
                _context.BoardBoardSquares.AddRange(defaultBoardBoardSquares);
                _context.SaveChanges();
            }
         
            
        }

        private List<BoardSquare> ConfigureDefaultBoardSquares()
        {
            return new List<BoardSquare>()
            {
                new GoSquare(),
                new BuildablePropertySquare("MEDITERRANEAN AVENUE", "Brown", 30, 60, 2, 10, 30, 90, 160, 250, 50),
                new CommunityChestSquare(),
                new BuildablePropertySquare("BALTIC AVENUE", "Brown", 30, 60, 4, 20, 60, 180, 320, 450, 50),
                new TaxSquare(200),
                new RailRoadSquare("READING RAILROAD", "Black", 100, 200),
                new BuildablePropertySquare("ORIENTAL AVENUE", "Light Blue", 50, 100, 6, 30, 90, 270, 400, 550, 50),
                new ChanceSquare(),
                new BuildablePropertySquare("VERMONT AVENUE", "Light Blue", 50, 100, 6, 30, 90, 270, 400, 550, 50),
                new BuildablePropertySquare("CONNECTICUT AVENUE", "Light Blue", 60, 120, 8, 40, 100, 300, 450, 600, 50),
                new JailSquare(),
                new BuildablePropertySquare("ST. CHARLES PLACE", "Pink", 70, 140, 10, 50, 150, 450, 625, 750, 100),
                new UtilitySquare("ELECTRIC COMPANY", "White", 75,  150, 4),
                new BuildablePropertySquare("STATES AVENUE", "Pink", 70, 140, 10, 50, 150, 450, 625, 750, 100),
                new BuildablePropertySquare("VIRGINIA AVENUE", "Pink", 80, 160, 12, 60, 180, 500, 700, 900, 100),
                new RailRoadSquare("PENNSYLVANIA RAILROAD", "Black", 100, 200),
                new BuildablePropertySquare("ST. JAMES PLACE", "Orange", 90, 180, 14, 70, 200, 550, 750, 950, 100),
                new CommunityChestSquare(),
                new BuildablePropertySquare("TENNESSEE AVENUE", "Orange", 90, 180, 14, 70, 200, 550, 750, 950, 100),
                new BuildablePropertySquare("NEW YORK AVENUE", "Orange", 100, 200, 16, 80, 220, 600, 800, 1000, 100),
                new FreeParkingSquare(),
                new BuildablePropertySquare("KENTUCKY AVENUE", "Red", 110, 220, 18, 90, 250, 700, 875, 1050, 150),
                new ChanceSquare(),
                new BuildablePropertySquare("INDIANA AVENUE", "Red", 110, 220, 18, 90, 250, 700, 875, 1050, 150),
                new BuildablePropertySquare("ILLINOIS AVENUE", "Red", 120, 240, 20, 100, 300, 750, 925, 1100, 150),
                new RailRoadSquare("B. & O. RAILROAD", "Black", 100, 200),
                new BuildablePropertySquare("ATLANTIC AVENUE", "Yellow", 130, 260, 22, 110, 330, 800, 975, 1150, 150),
                new BuildablePropertySquare("VENTNOR AVENUE", "Yellow", 130, 260, 22, 110, 330, 800, 975, 1150, 150),
                new UtilitySquare("WATER WORKS", "White", 75,  150, 4),
                new BuildablePropertySquare("MARVIN GARDENS", "Yellow", 140, 280, 24, 120, 360, 850, 1025, 1200, 150),
                new GoToJailSquare(),
                new BuildablePropertySquare("PACIFIC AVENUE", "Green", 150, 300, 26, 130, 390, 900, 1100, 1275, 200),
                new BuildablePropertySquare("NORTH CAROLINA AVENUE", "Green", 150, 300, 26, 130, 390, 900, 1100, 1275, 200),
                new CommunityChestSquare(),
                new BuildablePropertySquare("PENNSYLVANIA AVENUE", "Green", 160, 320, 28, 150, 450, 1000, 1200, 1400, 200),
                new RailRoadSquare("SHORT LINE", "Black", 100, 200),
                new ChanceSquare(),
                new BuildablePropertySquare("PARK PLACE", "Blue", 175, 350, 35, 175, 500, 1100, 1300, 1500, 200),
                new TaxSquare(75),
                new BuildablePropertySquare("BOARDWALK", "Blue", 200, 400, 50, 200, 600, 1400, 1700, 2000, 200)
            };
        }
        private Board CreateDefaultBoard()
        {
            return new Board("Default Board", null);
        }
        private List<BoardBoardSquare> LinkBoardSquaresToBoard(List<BoardSquare> boardSquares, Board board)
        {
            List<BoardBoardSquare> boardBoardSquares = new List<BoardBoardSquare>();
            int index = 0;
            foreach (BoardSquare square in boardSquares)
            {
                boardBoardSquares.Add(new BoardBoardSquare { BoardSquareId =  square.Id, BoardId = board.Id, Order = index++ });
            }
            return boardBoardSquares;
        }
        
        
    }
}
