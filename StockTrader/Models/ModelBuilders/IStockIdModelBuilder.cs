using System.Collections.Generic;

namespace StockTrader.Models.ModelBuilders
{
    public interface IStockIdModelBuilder
    {
        List<StockIdModel> ReadStoredStocks();

        List<StockIdModel> AddStockIdToStore(string stockName, int stockId);

        List<StockIdModel> RemoveStockFromStore(int index);
    }
}
