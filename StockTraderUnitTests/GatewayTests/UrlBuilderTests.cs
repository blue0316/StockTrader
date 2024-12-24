using StockTrader.APIHandler.HttpHandlers.UrlBuilder;
using StockTrader.APIHandler.Interfaces;
using StockTrader.Models.EnvVariables;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace StockTraderUnitTests.GatewayTests
{
    [TestFixture]
	public class UrlBuilderTests
	{
		private IUrlBuilder _urlBuilder;
		
		[SetUp]
		public void Setup()
		{

			var myOptions = Options.Create(new TradingView { Username = "Borneo_Invest", Password= "LqlekN4$4d58" });
			
			_urlBuilder = new UrlBuilder(myOptions);
		}

		[Test]
		public void BuildSearchUrlPurchaseMoreThan1000BuildsExpectedUrl()
		{
			// Arrange
			var purchasePrice = 1400;
			var playedId = 1234;
			var expected = $"https://www.tradingview.com/transfermarket?type=stock&maskedDefId=1234&macr=1300&num=21&start=0";

			// Act
			var actual = _urlBuilder.BuildSearchUrl(playedId, purchasePrice);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void BuildSearchWithPurchaseLessThan1000BuildsExpectedUrl()
		{
			// Arrange
			var purchasePrice = 900;
			var playedId = 1234;
			var expected = "https://www.tradingview.com/transfermarket?type=stock&maskedDefId=1234&macr=850&num=21&start=0";

			// Act
			var actual = _urlBuilder.BuildSearchUrl(playedId, purchasePrice);

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void BuildPurchaseUrlTakesParametersAndBuildsExpectedUrl()
		{
			//Arrange
			var tradeId = "12345";
			var expected = "https://www.tradingview.com/trade/12345/purchase";

			//Act
			var actual = _urlBuilder.BuildPurchaseUrl(tradeId);

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void BuildDeleteUrlTakesTradeIdsAndReturnsExpectedUrl()
		{
			//Arrange
			var tradeIds = "12345,54321";
			var expected = "https://www.tradingview.com/watchlist?tradeId=12345,54321";

			//Act
			var actual = _urlBuilder.BuildDeleteStockUrl(tradeIds);

			//Assert
			Assert.AreEqual(expected, actual);
		}
	}
}
