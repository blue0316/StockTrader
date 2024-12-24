using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StockTrader.Models
{
    public class SymbolSearchModel
    {
        [JsonPropertyName("symbolInfo")]
        public List<PurchaseViewModel> SymbolInfo { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("credits")]
        public int Credits { get; set; }
    }
}
