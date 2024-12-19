using StockComm.Models;

namespace StockComm.Repository.IRepository
{
    public interface IUserPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);

        Task<UserPortfolio> CreatePortfolioAsync (UserPortfolio userPortfolio);

        Task<UserPortfolio> DeletePortfolio(AppUser appUser, string companyName);
    }
}
