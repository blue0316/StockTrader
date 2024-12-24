using Newtonsoft.Json;

namespace StockTrader.Models
{
    public class PurchasePutModel
    {
        [JsonProperty("purchase")]
        public int PurchasePrice { get; set; }
    }
}
