using Microsoft.AspNetCore.Identity;

namespace StockComm.Models
{
    public class AppUser : IdentityUser
    {
        public List<UserPortfolio> Portfolios { get; set; } = new List<UserPortfolio>();
    }
}
