using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace StockTrader.Models.ModelBuilders
{
    public class StockIdModelBuilder : IStockIdModelBuilder
    {
        public List<StockIdModel> AddStockIdToStore(string stockName, int stockId)
        {
            var stocks = new List<StockIdModel>();
            var newStock = new StockIdModel
            {
                StockId = stockId,
                StockName = stockName
            };

            try
            {
                using (StreamReader r = new StreamReader("Data/StockIds.json"))
                {
                    string json = r.ReadToEnd();
                    stocks = JsonConvert.DeserializeObject<List<StockIdModel>>(json);
                    
                    stocks.Add(newStock);
                }
            }
            catch (Exception e)
            {
                return new List<StockIdModel>();
            }

            try
            {
                File.WriteAllText("Data/StockIds.json", JsonConvert.SerializeObject(stocks));
                return stocks;
            }
            catch(Exception e)
            {
                return new List<StockIdModel>();
            }
        }

        public List<StockIdModel> ReadStoredStocks()
        {
            try
            {
                using (StreamReader r = new StreamReader("Data/StockIds.json"))
                {
                    string json = r.ReadToEnd();
                    List<StockIdModel> stocks = JsonConvert.DeserializeObject<List<StockIdModel>>(json);

                    return stocks;
                }
            }
            catch (Exception e)
            {
                return new List<StockIdModel>();
            }

        }

        public List<StockIdModel> RemoveStockFromStore(int index)
        {
            var stocks = new List<StockIdModel>();

            try
            {
                using (StreamReader r = new StreamReader("Data/StockIds.json"))
                {
                    string json = r.ReadToEnd();
                    stocks = JsonConvert.DeserializeObject<List<StockIdModel>>(json);
                }
            }
            catch (Exception e)
            {
                return new List<StockIdModel>();
            }

            try
            {
                stocks.RemoveAt(index);
                File.WriteAllText("Data/StockIds.json", JsonConvert.SerializeObject(stocks));
                return stocks;
            }
            catch (Exception e)
            {
                return new List<StockIdModel>();
            }
        }
    }
}
