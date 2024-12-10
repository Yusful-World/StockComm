using Microsoft.AspNetCore.Mvc;
using StockComm.Data;

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
            var stocks = _db.Stocks.ToList();
            
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetStockById([FromRoute] int id)
        {
            var stockFromDb = _db.Stocks.Find(id);

            return Ok(stockFromDb);
        }
    }
}
