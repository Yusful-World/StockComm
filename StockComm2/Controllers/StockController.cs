using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using StockComm.Data;
using StockComm.Dtos.StockDtos;
using StockComm.Mappers;
using System.ComponentModel;

namespace StockComm.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public StockController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllStocks()
        {
            var listOfStocks = _db.Stocks.ToList().Select(stock => stock.ToStockDto());


            return Ok(listOfStocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetStockById([FromRoute] int id)
        {
            var stockFromDb = _db.Stocks.Find(id);
            if (stockFromDb == null)
            {
                return NotFound();
            }

            return Ok(stockFromDb.ToStockDto());
        }

        [HttpPost]
        [DisplayName("Create New Customer")]
        public IActionResult CreateStock([FromBody] CreateStockRequestDto stockRequestDto)
        {
            var createStock = stockRequestDto.ToCreateStockRequestDto();

            _db.Stocks.Add(createStock);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetStockById), new { id = createStock.Id }, createStock.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateStock([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
        {
            var updateStock = _db.Stocks.FirstOrDefault(stock => stock.Id == id);

            if (updateStock == null)
            {
                return NotFound("This stock does not exist");
            }

            updateStock.Symbol = updateStockDto.Symbol;
            updateStock.CompanyName = updateStockDto.CompanyName;
            updateStock.Purchase = updateStockDto.Purchase;
            updateStock.LastDividend = updateStockDto.LastDividend;
            updateStock.MarketCapital = updateStockDto.MarketCapital;
            updateStock.Industry = updateStockDto.Industry;

            _db.SaveChanges();
            return Ok(updateStock.ToStockDto());
        }
    }
}
