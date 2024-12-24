using StockTrader.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockTrader.APIHandler.Interfaces
{
    public interface IApiGateway
    {
        Task<List<PurchaseViewModel>> FetchStocks(int stockId, int purchasePrice, string accessToken);

        Task<List<PurchaseViewModel>> FetchStocksByLeague(int leagueId, int rarityId, int purchasePrice, string accessToken, string positionId, int nationId);

        Task<string> PurchaseOnStock(string tradeId, int purchasePrice, string accessToken);

        Task<SymbolSearchModel> GetSellStocks(string accessToken);

        Task<SymbolSearchModel> GetTransferList(string accessToken);

        Task<SymbolSearchModel> GetUnassignedPile(string accessToken);

        Task<string> SellStock(string tradeId, string stockId, string accessToken, int startPrice, int BinPrice);

        Task<string> CheckToken(string accessToken);

        Task ClearExpiredStocks(string accessToken, List<PurchaseViewModel> allStocks);
    }
}
