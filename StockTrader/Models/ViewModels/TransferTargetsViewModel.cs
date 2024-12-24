using System.Collections.Generic;

namespace StockTrader.Models.ViewModels
{
    public class SellStocksViewModel
    {
        public int orders { get; set; }

        public List<PurchaseViewModel> Purchases { get; set; }

        public int numberOfStocks { get; set; }
    }
}
