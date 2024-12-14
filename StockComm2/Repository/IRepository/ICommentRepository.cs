using StockComm.Models;

namespace StockComm.Repository.IRepository
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);

        Task<Comment> CreateAsync(Comment newComment);
        Task<Comment?> UpdateAsync(int id, Comment updateComment);
        Task<Comment?> DeleteAsync(int id);
    }
}
