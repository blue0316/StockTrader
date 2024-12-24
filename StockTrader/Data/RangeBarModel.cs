using System;
using System.Diagnostics;

using TradingViewUdfProvider.Utils;

namespace StockTrader.Data
{
    [DebuggerDisplay("Bar {Index} {TimestampDate} {CurrentPrice}")]
    public class RangeBarModel
    {
        private DateTime? _date;

        public double Timestamp { get; set; }

        public DateTime? Date
        {
            get => _date;
            set
            {
                if (value == null)
                    return;

                _date = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
                Timestamp = _date.Value.ToUnixSeconds();
            }
        }

        public double? Mid { get; set; }
        public double? Bid { get; set; }
        public double? Ask { get; set; }
        public double? Open { get; set; }
        public double? High { get; set; }
        public double? Low { get; set; }
        public double? Close { get; set; }
        public double? Volume { get; set; }
        public double CurrentPrice => Close ?? Mid ?? 0;
        public double InitialPrice => Open ?? Mid ?? 0;
        public int Index { get; set; }
        public DateTime TimestampDate => Date ?? TvDateTimeConverter.ConvertFromUnixSeconds(Timestamp);
    }
}
