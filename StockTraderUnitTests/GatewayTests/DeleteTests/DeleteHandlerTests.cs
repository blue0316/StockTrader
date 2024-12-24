using FakeItEasy;
using StockTrader.APIHandler.HttpHandlers.DeleteRequests;
using StockTrader.APIHandler.Interfaces;
using StockTrader.Models;
using StockTrader.Models.ModelBuilders;
using NUnit.Framework;
using System.Collections.Generic;

namespace StockTraderUnitTests.GatewayTests.DeleteTests
{
    [TestFixture]
    public class DeleteHandlerTests
    {
        private IDeleteHandler _deleteHandler;
        private IDeleteMaker _deleteMaker;
        private ITradeIdsBuilder _tradeIdsBuilder;
        private IUrlBuilder _urlBuilder;

        [SetUp]
        public void Setup()
        {
            _deleteMaker = A.Fake<IDeleteMaker>();
            _tradeIdsBuilder = A.Fake<ITradeIdsBuilder>();
            _urlBuilder = A.Fake<IUrlBuilder>();
            _deleteHandler = new DeleteHandler(_deleteMaker, _tradeIdsBuilder, _urlBuilder);
        }

        [Test]
        public void DeleteExpiredStocksCallsDeleteMakerAndDoesNotThrowError()
        {
            //Arrange
            var token = "Abc";
            var stocks = new List<PurchaseViewModel>();
            var ids = new TradeIdsModel
            {
                TradeIds = "12345"
            };
            A.CallTo(() => _tradeIdsBuilder.GetTradeIds(stocks)).Returns(ids);
            A.CallTo(() => _urlBuilder.BuildDeleteStockUrl(ids.TradeIds)).Returns("DeleteUrl");
            //Act

            //Assert
            Assert.DoesNotThrow(() => _deleteHandler.DeleteExpiredStocks(token, stocks));
            A.CallTo(() => _deleteMaker.MakeDeleteRequest(token, "DeleteUrl")).MustHaveHappenedOnceExactly();
        }
    }
}
