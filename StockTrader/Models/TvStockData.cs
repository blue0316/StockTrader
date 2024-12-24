using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockTrader.Models
{
    /// <summary>
    /// A class designed to retrieve historical stock price data from Trading View.
    /// </summary>
    public class TvStockData : IStockDataProvider
	{
		/// <summary>
		/// Gets historical prices from Trading View given the specified configuration.
		/// </summary>
		/// <param name="config">Configuration parameters used to retrieve historical price data.</param>
		/// <returns></returns>
		public async Task<List<StockData>> GetPricesAsync(IHistoryConfig config)
		{
			string Symbol = config.Ticker.ToString();
            TimeFrame Timescale = TimeFrame.Daily;
            List<StockData> prices = new List<StockData>();

            /*
                        IList<Candle> hist = await TvAPI.GetAsync(Symbol, config.DateFrom, config.DateTo, Timescale);



                        foreach (Candle candle in hist)
                        {
                            prices.Add(new StockData()
                            {
                                Ticker = Symbol,
                                Date = candle.DateTime,
                                Open = (double)candle.Open,
                                High = (double)candle.High,
                                Low = (double)candle.Low,
                                Close = (double)candle.Close,
                                CloseAdj = (double)candle.AdjustedClose,
                                Volume = candle.Volume
                            });
                        }
            */
            return prices;

		}
	}
}
