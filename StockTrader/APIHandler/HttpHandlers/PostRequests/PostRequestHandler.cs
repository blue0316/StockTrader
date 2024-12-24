using StockTrader.APIHandler.Interfaces;
using StockTrader.Models;
using System.Threading.Tasks;

namespace StockTrader.APIHandler.HttpHandlers.PostRequests
{
    public class PostRequestHandler : IPostRequestHandler
    {
        private readonly IPostRequestMaker _postRequestMaker;
        private readonly IUrlBuilder _urlBuilder;
        private readonly IStatusCodeHandler _statusCodeHandler;

        public PostRequestHandler(IPostRequestMaker postRequestMaker,IUrlBuilder urlBuilder,
            IStatusCodeHandler statusCodeHandler)
        {
            _postRequestMaker = postRequestMaker;
            _urlBuilder = urlBuilder;
            _statusCodeHandler = statusCodeHandler;
        }
        public async Task<string> SellStock(string stockId, string accessToken, int startPrice, int binPrice)
        {
            var url = _urlBuilder.GetSymbolUrl();
            var sellModel = new SellStockModel
            {
                StartPrice = startPrice,
                BinPrice = binPrice,
                Duration = 3600,
                ItemData = new SellItemDataModel
                {
                    stockId = stockId
                }
            };

            var statusCode = await _postRequestMaker.SellStock(url, accessToken, sellModel);

            var sellMessage = _statusCodeHandler.HandleSellingStatusCode(statusCode);

            return sellMessage;
        }
    }
}
