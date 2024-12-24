using System.Net;

namespace StockTrader.APIHandler.Interfaces
{
    public interface IStatusCodeHandler
    {
        string HandlePurchaseStockStatusCode(HttpStatusCode statusCode);

        string HandleSellingStatusCode(HttpStatusCode statusCode);

        string HandleTokenCheckStatusCode(HttpStatusCode statusCode);
    }
}
