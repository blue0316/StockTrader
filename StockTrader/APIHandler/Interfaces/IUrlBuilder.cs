namespace StockTrader.APIHandler.Interfaces
{
    public interface IUrlBuilder
    {
        public string BuildSearchUrl(int stockId, int purchasePrice);
        public string BuildSearchForLeagueRarityUrl(int leagueId, int rarityId, int purchasePrice, string positionId, int nationId);
        public string BuildPurchaseUrl(string tradeId);
        public string GetSellStocksUrl();
        public string GetItemUrl();
        public string GetSymbolUrl();
        public string GetCheckTokenUrl();
        public string BuildDeleteStockUrl(string tradeIds);
        public string GetTransferListUrl();

        public string GetUnassignedPileUrl();
    }
}
