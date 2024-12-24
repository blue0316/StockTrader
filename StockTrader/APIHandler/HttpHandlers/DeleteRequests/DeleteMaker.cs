using StockTrader.APIHandler.Interfaces;
using System.Threading.Tasks;

namespace StockTrader.APIHandler.HttpHandlers.DeleteRequests
{
    public class DeleteMaker : IDeleteMaker
    {
        private readonly IHttpWrapper _wrapper;

        public DeleteMaker(IHttpWrapper wrapper)
        {
            _wrapper = wrapper;
        }
        public async Task MakeDeleteRequest(string token, string url)
        {
            _wrapper.SetAccessToken(token);
            var response = await _wrapper.DeleteAsync(url);
        }
    }
}
