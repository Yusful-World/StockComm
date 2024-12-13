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
            var stockFromDb = _db.Stocks.FirstOrDefault(stock => stock.Id == id);

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

            _db.SaveChanges();
            return Ok(stockFromDb.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteStock([FromRoute] int id)
        {
            var stockFromDb = _db.Stocks.FirstOrDefault(s => s.Id == id);
            if (stockFromDb == null)
            {
                return NotFound();
            }

            _db.Stocks.Remove(stockFromDb);
            _db.SaveChanges();

            return Ok();
        }
    }
}
