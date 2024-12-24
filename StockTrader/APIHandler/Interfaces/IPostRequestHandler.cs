using System.Threading.Tasks;

namespace StockTrader.APIHandler.Interfaces
{
    public interface IPostRequestHandler
    {
        Task<string> SellStock(string stockId, string accessToken, int startPrice, int BinPrice);
    }
}
