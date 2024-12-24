using StockTrader.APIHandler.Interfaces;
using StockTrader.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockTrader.APIHandler.HttpHandlers.PutRequests
{
    // Ref. TradingView REST API Specification for Brokers :  tradingview.com/rest-api-spec/
    public class PutRequestMaker : IPutRequestMaker
    {
        private readonly IHttpWrapper _wrapper;

        public PutRequestMaker(IHttpWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        public async Task<HttpStatusCode> MoveStockToTradePile(string url, string accessToken, MoveStockBodyModel stockDetails)
        {
            _wrapper.SetAccessToken(accessToken);

            //var listData = new List<MoveStockBodyModel>
            //{
            //    stockDetails
            //};

            var stringContent = new StringContent(JsonConvert.SerializeObject(stockDetails));

            var response = await _wrapper.PutAsync(url, stringContent);

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> PutPurchaseOnStock(string url, string accessToken, int purchasePrice)
        {
            _wrapper.SetAccessToken(accessToken);

            var purchaseModel = new PurchasePutModel
            {
                PurchasePrice = purchasePrice
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(purchaseModel));

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("client_id", accessToken);
                    var response = await client.PutAsync(url, stringContent);

                    return response.StatusCode;
                }
            }
            catch (System.Exception)
            {
                return HttpStatusCode.Unauthorized;
            }
           
            //var response = await _wrapper.PutAsync(url, stringContent);

            //return response.StatusCode;
        }
    }
}
