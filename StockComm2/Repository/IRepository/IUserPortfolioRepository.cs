using StockComm.Models;

namespace StockComm.Repository.IRepository
{
    public interface IUserPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
    }
}
