using System.ComponentModel.DataAnnotations;

namespace StockComm.Dtos.CommentDtos
{
    public class UpdateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title cannot be less than 5 characters.")]
        [MaxLength(255, ErrorMessage = "Title cannot be more than 255 characters")]
        public string Title { get; set; } 

        [Required]
        [MinLength(5, ErrorMessage = "Comment cannot be less than 5 characters.")]
        [MaxLength(255, ErrorMessage = "Comment cannot be more than 255 characters")]
        public string Content { get; set; } 
    }
}
