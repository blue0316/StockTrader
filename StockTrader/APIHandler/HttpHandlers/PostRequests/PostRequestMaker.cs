using StockTrader.APIHandler.Interfaces;
using StockTrader.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockTrader.APIHandler.HttpHandlers.PostRequests
{
    public class PostRequestMaker : IPostRequestMaker
    {
        private readonly IHttpWrapper _wrapper;

        public PostRequestMaker(IHttpWrapper wrapper)
        {
            _wrapper = wrapper;
        }
        public async Task<HttpStatusCode> SellStock(string url, string accessToken, SellStockModel sellStockModel)
        {
            _wrapper.SetAccessToken(accessToken);
            var body = new StringContent(JsonConvert.SerializeObject(sellStockModel));
            var response = await _wrapper.PostAsync(url, body);

            return response.StatusCode;
        }
    }
}
