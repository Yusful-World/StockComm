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

        public async Task<UserPortfolio> CreatePortfolioAsync(UserPortfolio userPortfolio)
        {
            await _db.UserPortfolios.AddAsync(userPortfolio);
            await _db.SaveChangesAsync();

            return userPortfolio;
        }

        public async Task<UserPortfolio> DeletePortfolio(AppUser appUser, string companyName)
        {
            var portfolioModel = await _db.UserPortfolios.FirstOrDefaultAsync(p => p.AppUserId == appUser.Id && p.Stock.CompanyName.ToLower() == companyName.ToLower());

            if (portfolioModel == null)
                return null;

            _db.UserPortfolios.Remove(portfolioModel);
            await _db.SaveChangesAsync();

            return portfolioModel;
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
