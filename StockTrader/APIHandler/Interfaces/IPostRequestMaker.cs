using StockTrader.Models;
using System.Net;
using System.Threading.Tasks;

namespace StockTrader.APIHandler.Interfaces
{
    public interface IPostRequestMaker
    {
        Task<HttpStatusCode> SellStock(string url, string accessToken, SellStockModel sellStockModel);
    }
}
