using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using StockComm.Data;
using StockComm.Dtos.StockDtos;
using StockComm.Helpers;
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
        public async Task<IActionResult> GetAllStocks([FromQuery] QueryObject query)
        {
            var stocksFromDb = await _stockRepo.GetAllAsync(query);
            
            var listOfStocks = stocksFromDb.Select(stock => stock.ToStockDto());
            

            return Ok(listOfStocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            var stockFromDb = await _stockRepo.GetByIdAsync(id);

            return Ok(stockFromDb.ToStockDto());
        }

        [HttpPost]
        [DisplayName("Create New Stock")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createStock = stockRequestDto.ToCreateStockRequestDto();
            await _stockRepo.CreateAsync(createStock);

            return CreatedAtAction(nameof(GetStockById), new { id = createStock.Id }, createStock.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockFromDb = await _stockRepo.UpdateAsync(id, updateStockDto);

            await _db.SaveChangesAsync();
            
            return Ok(stockFromDb.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stockFromDb = await _stockRepo.DeleteAsync(id);

            if (stockFromDb == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
