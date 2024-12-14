using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StockComm.Data;
using StockComm.Dtos.StockDtos;
using StockComm.Helpers;
using StockComm.Models;
using StockComm.Repository.IRepository;
using System.Linq;

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

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stockList = _db.Stocks.Include(c => c.Comments).AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stockList = stockList.Where(s =>  s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stockList = stockList.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                {
                    stockList = query.IsDescending ? stockList.OrderByDescending(s => s.CompanyName) : stockList.OrderBy(s => s.CompanyName);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stockList.Skip(skipNumber).Take(query.PageSize).ToListAsync();
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
