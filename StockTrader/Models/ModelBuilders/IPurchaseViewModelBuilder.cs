using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockTrader.Models.ModelBuilders
{
    public interface IPurchaseViewModelBuilder
    {
        List<PurchaseViewModel> ConvertSearchModelToPurchaseView(List<StockSearchModel> searchModels);

        List<PurchaseViewModel> PopulateDefaultFieldsOfPurchaseViews(List<PurchaseViewModel> purchaseViews);
    }
}
