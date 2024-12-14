using StockComm.Models;

namespace StockComm.Repository.IRepository
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
    }
}
