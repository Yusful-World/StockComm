using StockComm.Dtos.StockDtos;
using StockComm.Helpers;
using StockComm.Models;

namespace StockComm.Repository.IRepository
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);

        Task<Stock?> GetByCompanyNameAsync(string conpanyName);

        Task<Stock> CreateAsync(Stock stock); 

        Task<Stock?> UpdateAsync(int id, UpdateStockDto updateStockDto);
        Task<Stock?> DeleteAsync(int id);

        Task<bool> StockExists (int id);

    }
}
