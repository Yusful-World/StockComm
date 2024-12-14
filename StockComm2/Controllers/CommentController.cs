using Microsoft.AspNetCore.Mvc;
using StockComm.Repository.IRepository;
using StockComm.Mappers;
using StockComm.Dtos.CommentDtos;
using StockComm.Models;

namespace StockComm.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var commentsFromDb = await _commentRepo.GetAllAsync();
            var listOfComments = commentsFromDb.Select(comment => comment.ToCommentDto());

            return Ok(listOfComments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var commentFromDb = await _commentRepo.GetByIdAsync(id);

            if (commentFromDb == null)
            {
                return NotFound();
            }

            return Ok(commentFromDb.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exist");
            }

            var newComment = commentDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(newComment);
            
            return CreatedAtAction(nameof(GetCommentById), new { id = newComment.Id }, newComment.ToCommentDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.UpdateAsync(id, updateCommentDto.ToCommentFromUpdate());
            if (comment == null)
            {
                return NotFound("Comment does not exist");
            }

            return Ok(comment.ToCommentDto());

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var CommentToBeDeleted = await _commentRepo.DeleteAsync(id);
            if (CommentToBeDeleted == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
