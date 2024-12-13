using Microsoft.EntityFrameworkCore;
using StockComm.Data;
using StockComm.Models;
using StockComm.Repository.IRepository;

namespace StockComm.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<Comment>> GetAllAsync()
        {
            return await _db.Comments.ToListAsync();
        }
    }
}
