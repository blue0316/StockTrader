using System.Collections.Generic;
using System.Linq;

namespace StockTrader.Models.ModelBuilders
{
    public class PurchaseViewModelBuilder : IPurchaseViewModelBuilder
    {
        public List<PurchaseViewModel> ConvertSearchModelToPurchaseView(List<StockSearchModel> searchModels)
        {
            if (searchModels.Count < 1)
            {
                return new List<PurchaseViewModel>
                {
                    new PurchaseViewModel
                    {
                        Status = "Error",
                        Pending = false,
                    }
                };
            }

            return searchModels.Select(x => new PurchaseViewModel
            {
                Status = "Pending",
                Pending = true,
                TimeRemaining = x.TimeRemaining,
                TradeId = x.TradeId
            }).ToList();
        }

        public List<PurchaseViewModel> PopulateDefaultFieldsOfPurchaseViews(List<PurchaseViewModel> stocks)
        {

            foreach(var stock in stocks)
            {
                if (stock.TimeRemaining == -1)
                {
                    stock.Pending = false;
                }
                if(stock.Status == "none")
                {
                    stock.Status = stock.TradeState;
                }
            }

            return stocks;
        }
    }
}
