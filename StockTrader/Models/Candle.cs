using System;

namespace StockTrader.Models
{
    public class Candle
    {
        public string Ticker { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public double AdjustedClose { get; set; }
        public DateTime DateTime { get; set; }
    }
}