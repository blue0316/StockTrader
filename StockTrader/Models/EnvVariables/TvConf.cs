namespace StockTrader.Models.EnvVariables
{
    public class TvConf
    {
        public const string Location = "https://www.tradingview.com/";
        public string Username { get; set; }

        public string Password { get; set; }

        public string accountId { get; set; }
    }
}
