﻿using System.ComponentModel.DataAnnotations.Schema;

namespace StockComm.Models
{
    [Table("Stocks")]
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Purchase {  get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal LastDividend { get; set; }
        public long MarketCapital { get; set; }
        public string Industry { get; set; } = string.Empty; 
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<UserPortfolio> Portfolios { get; set; } = new List<UserPortfolio>();

    }
}
