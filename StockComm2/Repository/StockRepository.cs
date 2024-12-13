using Microsoft.EntityFrameworkCore;
using StockComm.Data;
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
        public Task<List<Stock>> GetAllAsync()
        {
            return _db.Stocks.ToListAsync();
        }
    }
}
