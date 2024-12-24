using StockTrader.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockTrader.APIHandler.Interfaces
{
    public interface IGetRequestHandler
    {
        Task<List<PurchaseViewModel>> SearchForSpecificStock(int stockId, int purchasePrice, string accessToken);

        Task<List<PurchaseViewModel>> SearchForLeagueRarityStocks(int leagueId, int rarityId, int purchasePrice, string accessToken, string positionId, int nationId);

        Task<SymbolSearchModel> GetSellStocks(string accessToken);

        Task<SymbolSearchModel> GetTransferList(string accessToken);

        Task<SymbolSearchModel> GetUnassignedPile(string accessToken);

        Task<string> CheckToken(string accessToken);
    }
}
