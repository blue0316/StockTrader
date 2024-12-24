using StockTrader.APIHandler.Interfaces;
using StockTrader.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace StockTrader.APIHandler.HttpHandlers.PutRequests
{
    public class PutRequestHandler : IPutRequestHandler
    {
        private readonly IUrlBuilder _urlBuilder;
        private readonly IPutRequestMaker _putRequestMaker;
        private readonly IStatusCodeHandler _statusCodeHandler;

        public PutRequestHandler(IUrlBuilder urlBuilder, IPutRequestMaker putRequestMaker,
            IStatusCodeHandler statusCodeHandler)
        {
            _urlBuilder = urlBuilder;
            _putRequestMaker = putRequestMaker;
            _statusCodeHandler = statusCodeHandler;
        }

        public async Task<HttpStatusCode> MoveStockToTradePile(string tradeId, string stockId, string accessToken)
        {
            var url = _urlBuilder.GetItemUrl();
            var moveModel = new MoveStockBodyModel
            {
                ItemData = new List<MoveStockDataModel>
                {
                    new MoveStockDataModel
                    {
                        Pile = "trade",
                        StockId = stockId,
                        tradeId = tradeId
                    }

                }
            };

            var response = await _putRequestMaker.MoveStockToTradePile(url, accessToken, moveModel);

            return response;
        }

        public async Task<string> PutPurchaseOnStock(string tradeId, int purchasePrice, string accessToken)
        {
            var url = _urlBuilder.BuildPurchaseUrl(tradeId);
            var purchaseResponse = await _putRequestMaker.PutPurchaseOnStock(url, accessToken, purchasePrice);
            var purchaseMessage = _statusCodeHandler.HandlePurchaseStockStatusCode(purchaseResponse);

            return purchaseMessage;
        }
    }
}
