using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using StockComm.Data;
using StockComm.Dtos.StockDtos;
using StockComm.Mappers;
using StockComm.Repository.IRepository;
using System.ComponentModel;

namespace StockComm.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDbContext db, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocksFromDb = await _stockRepo.GetAllAsync();
            
            var listOfStocks = stocksFromDb.Select(stock => stock.ToStockDto());
            

            return Ok(listOfStocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            var stockFromDb = await _db.Stocks.FindAsync(id);
            if (stockFromDb == null)
            {
                return NotFound();
            }

            return Ok(stockFromDb.ToStockDto());
        }

        [HttpPost]
        [DisplayName("Create New Customer")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockRequestDto)
        {
            var createStock = stockRequestDto.ToCreateStockRequestDto();

            await _db.Stocks.AddAsync(createStock);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStockById), new { id = createStock.Id }, createStock.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
        {
            var stockFromDb = await _db.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if (stockFromDb == null)
            {
                return NotFound("This stock does not exist");
            }

            stockFromDb.Symbol = updateStockDto.Symbol;
            stockFromDb.CompanyName = updateStockDto.CompanyName;
            stockFromDb.Purchase = updateStockDto.Purchase;
            stockFromDb.LastDividend = updateStockDto.LastDividend;
            stockFromDb.MarketCapital = updateStockDto.MarketCapital;
            stockFromDb.Industry = updateStockDto.Industry;

            await _db.SaveChangesAsync();
            return Ok(stockFromDb.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stockFromDb = await _db.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockFromDb == null)
            {
                return NotFound();
            }

            _db.Stocks.Remove(stockFromDb);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
