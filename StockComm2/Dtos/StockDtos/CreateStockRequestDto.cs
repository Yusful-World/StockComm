using System.ComponentModel.DataAnnotations;

namespace StockComm.Dtos.StockDtos
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be more than 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(30, ErrorMessage = "Company name be more than 30 characters")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000000000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.01, 1000)]
        public decimal LastDividend { get; set; }

        [Required]
        [Range(1, 5000000000)]
        public long MarketCapital { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage ="Industry cannot be more than 30 characters.")]
        public string Industry { get; set; } = string.Empty;
    }
}
