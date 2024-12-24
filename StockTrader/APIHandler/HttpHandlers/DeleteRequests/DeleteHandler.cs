using StockTrader.APIHandler.Interfaces;
using StockTrader.Models;
using StockTrader.Models.ModelBuilders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockTrader.APIHandler.HttpHandlers.DeleteRequests
{
    public class DeleteHandler : IDeleteHandler
    {
        private readonly IDeleteMaker _deleteMaker;
        private readonly ITradeIdsBuilder _tradeIdsBuilder;
        private readonly IUrlBuilder _urlBuilder;

        public DeleteHandler(IDeleteMaker deleteMaker, ITradeIdsBuilder tradeIdsBuilder, IUrlBuilder urlBuilder)
        {
            _deleteMaker = deleteMaker;
            _tradeIdsBuilder = tradeIdsBuilder;
            _urlBuilder = urlBuilder;
        }
        public async Task DeleteExpiredStocks(string accessToken, List<PurchaseViewModel> stocks)
        {
            var tradeIds = _tradeIdsBuilder.GetTradeIds(stocks);
            var url = _urlBuilder.BuildDeleteStockUrl(tradeIds.TradeIds);
            await _deleteMaker.MakeDeleteRequest(accessToken, url);
        }
    }
}
