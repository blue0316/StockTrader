using StockTrader.Models;
using System.Net;
using System.Threading.Tasks;

namespace StockTrader.APIHandler.Interfaces
{
    public interface IPutRequestMaker
    {
        Task<HttpStatusCode> PutPurchaseOnStock(string url, string accessToken, int purchasePrice);

        Task<HttpStatusCode> MoveStockToTradePile(string url, string accessToken, MoveStockBodyModel stockDetails);
    }
}
