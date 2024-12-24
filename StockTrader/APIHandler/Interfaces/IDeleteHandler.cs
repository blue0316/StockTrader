using StockTrader.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockTrader.APIHandler.Interfaces
{
    public interface IDeleteHandler
    {
        public Task DeleteExpiredStocks(string accessToken, List<PurchaseViewModel> stocks);
    }
}
