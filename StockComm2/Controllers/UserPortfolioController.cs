using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockComm.Extensions;
using StockComm.Models;
using StockComm.Repository.IRepository;

namespace StockComm.Controllers
{
    [Route("api/userportfolio")]
    [ApiController]
    public class UserPortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IUserPortfolioRepository _userPortfolioRepo;
        public UserPortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo, IUserPortfolioRepository userPortfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _userPortfolioRepo = userPortfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();  //User is a property inherited from the ControllerBase
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _userPortfolioRepo.GetUserPortfolio(appUser);
            
            return Ok(userPortfolio);
        }
    }
}
