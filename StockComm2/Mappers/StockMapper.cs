using StockComm.Dtos.StockDtos;
using StockComm.Models;

namespace StockComm.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto()
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDividend = stockModel.LastDividend,
                Industry = stockModel.Industry,
                MarketCapital = stockModel.MarketCapital
            };
        }

        public static Stock ToCreateStockRequestDto(this CreateStockRequestDto createStockDto)
        {
            return new Stock()
            {
                Symbol = createStockDto.Symbol,
                CompanyName = createStockDto.CompanyName,
                Purchase = createStockDto.Purchase,
                LastDividend = createStockDto.LastDividend,
                MarketCapital= createStockDto.MarketCapital,
                Industry= createStockDto.Industry
            };
        }
    }
}
