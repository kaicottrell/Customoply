using CustomMonopoly.Server.Data;
using CustomMonopoly.Server.Models.Cards;

namespace CustomMonopoly.Server.Services
{
    /// <summary>
    /// Handles functions corresponding with drawing cards and handling  the card events
    /// 
    /// </summary>
    public class CardService
    {
        private readonly ApplicationDbContext _context;
        public CardService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        //public CommunityChestCard DrawCommunityCard()
        //{
            
        //}
        //public ChanceCard DrawCommunityCard()
        //{

        //}
    }
}
