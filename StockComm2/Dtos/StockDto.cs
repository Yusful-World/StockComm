﻿using StockComm.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockComm.Dtos
{
    public class StockDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;

        public decimal Purchase { get; set; }

        public decimal LastDividend { get; set; }
        public long MarketCapital { get; set; }
        public string Industry { get; set; } = string.Empty;
    }
}
