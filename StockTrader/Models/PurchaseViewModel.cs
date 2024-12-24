
using Newtonsoft.Json;

namespace StockTrader.Models
{
    public class PurchaseViewModel
    {
        [JsonProperty("tradeId")]
        public string TradeId { get; set; }

        [JsonProperty("currentPurchase")]
        public int PurchasePrice { get; set; }

        [JsonProperty("purchaseState")]
        public string Status { get; set; }

        [JsonProperty("expires")]
        public int TimeRemaining { get; set; }

        public bool Pending { get; set; } = true;

        [JsonProperty("tradeState")]
        public string TradeState { get; set; }

        [JsonProperty("itemData")]
        public PurchaseViewItemData ItemData { get; set; }
    }

    public class PurchaseViewItemData
    {
        [JsonProperty("id")]
        public string stockId { get; set; }
    }
}
