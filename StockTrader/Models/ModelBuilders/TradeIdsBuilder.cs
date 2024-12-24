using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Models.ModelBuilders
{
    public class TradeIdsBuilder : ITradeIdsBuilder
    {
        public TradeIdsModel GetTradeIds(List<PurchaseViewModel> allStocks)
        {
            var ids = allStocks.Where(x => x.Status != "highest").Select(x => x.TradeId);

            return new TradeIdsModel
            {
                TradeIds = string.Join(",", ids)
            };
        }
    }
}
