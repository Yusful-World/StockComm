using StockComm.Dtos.StockDtos;
using StockComm.Models;

namespace StockComm.Repository.IRepository
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);

        Task<Stock> CreateAsync(Stock stock); 

        Task<Stock?> UpdateAsync(int id, UpdateStockDto updateStockDto);
        Task<Stock?> DeleteAsync(int id);

        Task<bool> StockExists (int id);

    }
}
