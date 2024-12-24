using System.Collections.Generic;

namespace StockTrader.Models.SearchOptions
{
    // List of the best-performing stocks by investment ranking related to PER
    // some best S&P 500 stocks as of April 2024
    public class StockPositions
    {
        public List<string> Positions { get; set; } = new List<string>
        {
            "SMCI",
            "NVDA",
            "CEG",
            "DECK",
            "MU",
            "GE",
            "META",
            "MPC",
            "DIS",
            "LLY"
        };
    }
}
