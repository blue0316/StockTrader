using System.Collections.Generic;

namespace StockTrader.Models.ModelBuilders
{
    public interface ITradeIdsBuilder
    {
        public TradeIdsModel GetTradeIds(List<PurchaseViewModel> allStocks);
    }
}
