using Microsoft.AspNetCore.Mvc;
using StockComm.Repository.IRepository;
using StockComm.Mappers;

namespace StockComm.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var commentsFromDb = await _commentRepo.GetAllAsync();
            var listOfComments = commentsFromDb.Select(comment => comment.ToCommentDto());

            return Ok(listOfComments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById([FromBody] int id)
        {
            var commentFromDb = await _commentRepo.GetByIdAsync(id);

            if (commentFromDb == null)
            {
                return NotFound();
            }

            return Ok(commentFromDb.ToCommentDto());
        }
    }
}
