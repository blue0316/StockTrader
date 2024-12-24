using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockTrader.Models
{
    /// <summary>
    /// A stock data provider used to retrieve historical price data from various sources (e.g. Trading View).
    /// </summary>
    public interface IStockDataProvider
	{
		Task<List<StockData>> GetPricesAsync(IHistoryConfig config);
	}
}
