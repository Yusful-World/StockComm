using Microsoft.EntityFrameworkCore;
using StockComm.Data;
using StockComm.Models;
using StockComm.Repository.IRepository;


namespace StockComm.Repository
{
    public class UserPortfolioRepository : IUserPortfolioRepository
    {
        private readonly ApplicationDbContext _db;
        public UserPortfolioRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<UserPortfolio> CreateAsync(UserPortfolio userPortfolio)
        {
            await _db.UserPortfolios.AddAsync(userPortfolio);
            await _db.SaveChangesAsync();

            return userPortfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _db.UserPortfolios.Where(u => u.AppUserId == user.Id)
                .Select(stock => new Stock
                {
                    Id = stock.StockId,
                    Symbol = stock.Stock.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    MarketCapital = stock.Stock.MarketCapital,
                    LastDividend = stock.Stock.LastDividend,
                    Purchase = stock.Stock.Purchase,
                    Industry = stock.Stock.Industry
                }).ToListAsync(); 
        }
    }
}
