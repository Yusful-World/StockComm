using System.ComponentModel.DataAnnotations.Schema;

namespace StockComm.Models
{
    [Table("UserPortfolios")]
    public class UserPortfolio
    {
        public string AppUserId { get; set; }
        public int StockId { get; set; }
        public AppUser AppUser { get; set; }
        public Stock Stock { get; set; } 
    }
}
