using StockTrader.APIHandler.Interfaces;
using StockTrader.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockTrader.APIHandler.HttpHandlers.GetRequests
{
    public class GetRequestHandler : IGetRequestHandler
    {
        private readonly IGetRequestMaker _getRequestMaker;
        private readonly IUrlBuilder _urlBuilder;
        private readonly IStatusCodeHandler _statusCodeHandler;

        public GetRequestHandler(IGetRequestMaker getRequestMaker, IUrlBuilder urlBuilder,
            IStatusCodeHandler statusCodeHandler)
        {
            _getRequestMaker = getRequestMaker;
            _urlBuilder = urlBuilder;
            _statusCodeHandler = statusCodeHandler;
        }

        public async Task<string> CheckToken(string accessToken)
        {
            var url = _urlBuilder.GetCheckTokenUrl();
            var responseCode = await _getRequestMaker.MakeGetRequestStatusCode(url, accessToken);
            return _statusCodeHandler.HandleTokenCheckStatusCode(responseCode);
        }

        public async Task<SymbolSearchModel> GetTransferList(string accessToken)
        {
            var url = _urlBuilder.GetTransferListUrl();

            try
            {
                var response = await _getRequestMaker.MakeGetRequest(url, accessToken);
                var symbolInfo = JsonConvert.DeserializeObject<SymbolSearchModel>(response);

                return symbolInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SymbolSearchModel();
            }
        }

        public async Task<SymbolSearchModel> GetSellStocks(string accessToken)
        {
            var url = _urlBuilder.GetSellStocksUrl();
            try
            {
                var response = await _getRequestMaker.MakeGetRequest(url, accessToken);
                var symbolInfo = JsonConvert.DeserializeObject<SymbolSearchModel>(response);

                return symbolInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SymbolSearchModel();
            }
        }

        public async Task<SymbolSearchModel> GetUnassignedPile(string accessToken)
        {
            var url = _urlBuilder.GetUnassignedPileUrl();
            try
            {
                var response = await _getRequestMaker.MakeGetRequest(url, accessToken);
                var symbolInfo = JsonConvert.DeserializeObject<SymbolSearchModel>(response);

                return symbolInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SymbolSearchModel();
            }
        }

        public async Task<List<PurchaseViewModel>> SearchForLeagueRarityStocks(int leagueId, int rarityId, int purchasePrice, string accessToken,
            string positionId, int nationId)
        {
            var url = _urlBuilder.BuildSearchForLeagueRarityUrl(leagueId, rarityId, purchasePrice, positionId, nationId);

            try
            {
                var response = await _getRequestMaker.MakeGetRequest(url, accessToken);
                var symbolInfo = JsonConvert.DeserializeObject<SymbolSearchModel>(response);

                return symbolInfo.SymbolInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<PurchaseViewModel>();
            }
        }

        public async Task<List<PurchaseViewModel>> SearchForSpecificStock(int stockId, int purchasePrice, string accessToken)
        {
            var url = _urlBuilder.BuildSearchUrl(stockId, purchasePrice);

            try
            {
                var response = await _getRequestMaker.MakeGetRequest(url, accessToken);
                var symbolInfo = JsonConvert.DeserializeObject<SymbolSearchModel>(response);

                return symbolInfo.SymbolInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<PurchaseViewModel>();
            }
        }
    }
}
