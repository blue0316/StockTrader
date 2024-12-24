using StockTrader.Models;
using System.Threading.Tasks;

namespace StockTrader.APIHandler.Interfaces
{
    public interface IDeleteMaker
    {
        public Task MakeDeleteRequest(string token, string uri);
    }
}
