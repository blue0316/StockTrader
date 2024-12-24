using Newtonsoft.Json;

namespace StockTrader.Models
{
    public class SellStockModel
    {
        [JsonProperty("startingPurchase")]
        public int StartPrice { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; } = 3600;

        [JsonProperty("buyNowPrice")]
        public int BinPrice { get; set; }

        [JsonProperty("itemData")]
        public SellItemDataModel ItemData { get; set; }
    }

    public class SellItemDataModel
    {
        [JsonProperty("id")]
        public string stockId { get; set; }
    }
}
