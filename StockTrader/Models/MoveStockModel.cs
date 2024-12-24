using Newtonsoft.Json;
using System.Collections.Generic;

namespace StockTrader.Models
{
    public class MoveStockBodyModel
    {
        [JsonProperty("itemData")]
        public List<MoveStockDataModel> ItemData { get; set; }
    }

    public class MoveStockDataModel
    {
        [JsonProperty("id")]
        public string StockId { get; set; }

        [JsonProperty("pile")]
        public string Pile { get; set; }

        [JsonProperty("tradeId")]
        public string tradeId { get; set; }
    }
}
