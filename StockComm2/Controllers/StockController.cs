using Microsoft.AspNetCore.Mvc;
using StockComm.Data;
using StockComm.Mappers;

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
    }
}
