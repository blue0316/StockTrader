using StockTrader.APIHandler.Interfaces;
using Microsoft.Extensions.Options;
using StockTrader.Models.EnvVariables;
using System.Collections.Generic;

namespace StockTrader.APIHandler.HttpHandlers.UrlBuilder
{

    public class UrlBuilder : IUrlBuilder
    {
        private TvConf _TVConfiguration;

        private string _searchBase;
        private string _purchaseBase;
        private string _watchListBase;
        private string _tradePileBase;
        private string _unassignedBase;

        private string instrument="EUR";
        private string side = "buy";
        private string type = "limit";
        private int qty = 1;
        private double limitPrice = 1.23456;
        private double stopPrice = 1.23456;
        private string  durationType = "gtt";
        private decimal durationDateTime = 1548406235;
        private double stopLoss = 1.23456;
        private decimal trailingStopPips = 0;
        private decimal guaranteedStop = 0;
        private decimal takeProfit = 0;
        private decimal digitalSignature = 954345868;
        private double currentAsk = 1.25;
        private double currentBid = 1.23;

        public UrlBuilder(IOptions<TvConf> tVConf)
        {
            _TVConfiguration = tVConf.Value;
            /*
                // Ref. tradingview.com/rest-api-spec
                $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/instruments
                $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/state
                $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/stream/state
                $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/orders
                $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/stream/orders
                $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/positions
                $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/stream/positions
                $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/netPositions
                $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/stream/netPositions
                $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/individualPositions
                $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/stream/individualPositions
                $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/balances
            */

            _searchBase = $"https://www.tradingview.com/symbol_info";
            _purchaseBase = $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/orders";
            _watchListBase = $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/netPositions";
            _tradePileBase = $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/individualPositions";
            _unassignedBase = $"https://www.tradingview.com/accounts/{_TVConfiguration.accountId}/stream/individualPositions";
        }

        // TODO : make the correct REST API implementations
        public string BuildPurchaseUrl(string tradeId)
        {
            var purchaseQuery = $"{tradeId}/purchase";

            return _purchaseBase + purchaseQuery;
        }

        public string BuildDeleteStockUrl(string tradeIds)
        {
            var query = $"?tradeId={tradeIds}";
            return _watchListBase + query;
        }

        public string BuildSearchForLeagueRarityUrl(int leagueId, int rarityId, int purchasePrice, string positionId, int nationId)
        {
            string parameterQuery;
            if (purchasePrice <= 1000)
            {
                parameterQuery = $"&rarityIds={rarityId}&lev=gold&macr={purchasePrice - 50}&num=21&start=0";
            }
            else
            {
                parameterQuery = $"&rarityIds={rarityId}&lev=gold&macr={purchasePrice - 100}&num=21&start=0";
            }

            if (leagueId != 0)
            {
                parameterQuery += $"&leag={leagueId}";
            }
            if (!string.IsNullOrEmpty(positionId))
            {
                parameterQuery += $"&pos={positionId}";
            }
            if(nationId != 0)
            {
                parameterQuery += $"&nat={nationId}";
            }

            return _searchBase + parameterQuery;
        }

        public string BuildSearchUrl(int stockId, int purchasePrice)
        {
            string parameterQuery;
            if (purchasePrice <= 1000)
            {
                parameterQuery = $"&maskedDefId={stockId}&macr={purchasePrice - 50}&num=21&start=0";
            }
            else
            {
                parameterQuery = $"&maskedDefId={stockId}&macr={purchasePrice - 100}&num=21&start=0";
            }
            return _searchBase + parameterQuery;
        }

        public string GetSymbolUrl()
        {
            return $"https://www.tradingview.com/{_TVConfiguration.accountId}/symbolhouse";
        }

        public string GetCheckTokenUrl()
        {
            return $"https://www.tradingview.com/{_TVConfiguration.accountId}/user/credits";
        }

        public string GetItemUrl()
        {
            return $"https://www.tradingview.com/{_TVConfiguration.accountId}/item";
        }

        public string GetTransferListUrl()
        {
            return _tradePileBase;
        }

        public string GetSellStocksUrl()
        {
            return _watchListBase;
        }

        public string GetUnassignedPileUrl()
        {
            return _unassignedBase;
        }
    }
}
