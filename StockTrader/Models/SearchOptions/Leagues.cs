using System.Collections.Generic;

namespace StockTrader.Models.SearchOptions
{
    // List of the stocks names with tickers
    public class Leagues
    {
        public Dictionary<string, string> LeagueOptions { get; set; } = new Dictionary<string, string>
        {
            {"Apple", "AAPL" },
            {"Google", "GOOG" },
            {"Microsoft", "MSFT" }
        };
    }
}
