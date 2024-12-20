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

        public async Task<Comment> CreateAsync(Comment newComment)
        {
            await _db.Comments.AddAsync(newComment);
            await _db.SaveChangesAsync();
            
            return newComment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentFromDb = await _db.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (commentFromDb == null)
            {
                return null;
            }

            _db.Comments.Remove(commentFromDb);
            await _db.SaveChangesAsync();

            return commentFromDb;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _db.Comments.Include(a => a.AppUser).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _db.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment updateComment)
        {
            var existingComment = await _db.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = updateComment.Title;
            existingComment.Content = updateComment.Content;

            _db.SaveChangesAsync(); 

            return existingComment;
        }
    }
}
