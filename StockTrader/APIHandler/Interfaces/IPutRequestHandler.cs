using System.Net;
using System.Threading.Tasks;

namespace StockTrader.APIHandler.Interfaces
{
    public interface IPutRequestHandler
    {
        Task<string> PutPurchaseOnStock(string tradeId, int purchasePrice, string accessToken);

        Task<HttpStatusCode> MoveStockToTradePile(string tradeId, string stockId, string accessToken);
    }
}
