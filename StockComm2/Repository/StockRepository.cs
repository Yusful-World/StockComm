using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StockComm.Data;
using StockComm.Dtos.StockDtos;
using StockComm.Models;
using StockComm.Repository.IRepository;

namespace StockComm.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _db;

        public StockRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Stock> CreateAsync(Stock newStock)
        {
            
            await _db.Stocks.AddAsync(newStock);
            await _db.SaveChangesAsync();

            return newStock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockFromDb = await _db.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (stockFromDb == null)
            {
                return null;
            }

            _db.Stocks.Remove(stockFromDb);
            await _db.SaveChangesAsync();

            return stockFromDb;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _db.Stocks.Include(c => c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stockFromDb = await _db.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
            if (stockFromDb == null)
            {
                return null;
            }

            return stockFromDb;
        }

        public async Task<bool> StockExists(int id)
        {
            return await _db.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto updateStockDto)
        {
            var stockFromDb = await _db.Stocks.FirstOrDefaultAsync(s =>s.Id == id);
            if (stockFromDb == null)
            {
                return null;
            }

            stockFromDb.Symbol = updateStockDto.Symbol;
            stockFromDb.CompanyName = updateStockDto.CompanyName;
            stockFromDb.Purchase = updateStockDto.Purchase;
            stockFromDb.LastDividend = updateStockDto.LastDividend;
            stockFromDb.MarketCapital = updateStockDto.MarketCapital;
            stockFromDb.Industry = updateStockDto.Industry;

            await _db.SaveChangesAsync();
            
            return stockFromDb;
        }
    }
}
