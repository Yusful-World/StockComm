using System.ComponentModel.DataAnnotations;

namespace StockComm.Dtos.AccountDtos
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
