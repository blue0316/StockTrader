using System.Collections.Generic;

namespace StockTrader.Models.SearchOptions
{
    // List of the composite index by nationality
    public class Nations
    {
        public Dictionary<string, int> NationOptions { get; set; } = new Dictionary<string, int>
        {
            {"CAC 40", 800 },
            {"NYSE Composite", 801 },
            {"SSE Composite", 802 },
            {"S&P 500", 803 },
            {"DAX 40", 804 },
            {"FTSE 100", 805 },
            {"Nikkei 225", 806 }
        };
    }
}
