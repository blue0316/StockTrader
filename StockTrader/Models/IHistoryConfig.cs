using System;

namespace StockTrader.Models
{
    /// <summary>
    /// Represents configuration parameters used to retrieve historical price data.
    /// </summary>
    public interface IHistoryConfig
	{
		Ticker Ticker { get; set; }
		TimeFrame TimeFrame { get; set; }
		DateTime DateFrom { get; set; }
		DateTime DateTo { get; set; }
	}
}