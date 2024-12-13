using StockComm.Models;

namespace StockComm.Repository.IRepository
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
    }
}
