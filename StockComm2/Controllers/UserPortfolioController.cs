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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUserPortfolio(string companyName)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null)
                return BadRequest("Require user log in.");  

            var stock = await _stockRepo.GetByCompanyNameAsync(companyName);

            if (stock == null)
                return NotFound("Stock not found");

            var userPortfolio = await _userPortfolioRepo.GetUserPortfolio(appUser);

            if (userPortfolio.Any(e => e.CompanyName.ToLower() == companyName.ToLower()))
                return BadRequest("Stock already exists in Portfolio");

            var portfolioModel = new UserPortfolio()
            {
                StockId = stock.Id,
                AppUserId = appUser.Id,
            };

            await _userPortfolioRepo.CreatePortfolioAsync(portfolioModel);

            if (portfolioModel == null)
            {
                return StatusCode(500, "Could not create portfolio");
            }
            else
            {
                return CreatedAtAction(nameof(GetUserPortfolio), new { id = portfolioModel.StockId}, "Portfolio was created successfully");
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUserPortfolio(string companyName)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var userPortfolio = await _userPortfolioRepo.GetUserPortfolio(appUser);

            var filteredStock = userPortfolio.Where(p => p.CompanyName.ToLower() == companyName.ToLower());

            if (!filteredStock.Any())
            {
                return NotFound("This stock not in your Portfolio");
            }
            else
            {
                await _userPortfolioRepo.DeletePortfolio(appUser, companyName);
            }

            return Ok();

        }
    }
}
