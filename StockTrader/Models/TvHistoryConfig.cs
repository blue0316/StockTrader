using System;


namespace StockTrader.Models
{
	/// <summary>
	/// Holds the configuration parameters used to retrieve historical stock prices from Trading View. 
	/// </summary>
	public class TvHistoryConfig : IHistoryConfig
	{
		public Ticker Ticker { get; set; }
		public TimeFrame TimeFrame { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }

		public TvHistoryConfig()
		{
			Ticker = Ticker.AMZN; // by default Amazon
			TimeFrame = TimeFrame.Daily;
			DateFrom = new DateTime(2018, 1, 1);
			DateTo = new DateTime(2018, 12, 31);
		}

	}
}