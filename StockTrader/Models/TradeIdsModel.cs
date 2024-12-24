using Newtonsoft.Json;

namespace StockTrader.Models
{
    public class TradeIdsModel
    {
        [JsonProperty("tradeid")]
        public string TradeIds { get; set; }
    }
}
