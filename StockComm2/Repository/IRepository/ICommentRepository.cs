using StockComm.Models;

namespace StockComm.Repository.IRepository
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
    }
}
