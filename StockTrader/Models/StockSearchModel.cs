using Newtonsoft.Json;

namespace StockTrader.Models
{
    public class StockSearchModel
    {
        [JsonProperty("tradeId")]
        public string TradeId { get; set; }

        [JsonProperty("expires")]
        public int TimeRemaining { get; set; }
    }
}
