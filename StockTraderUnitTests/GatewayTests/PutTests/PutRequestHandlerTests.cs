using FakeItEasy;
using StockTrader.APIHandler.HttpHandlers.PutRequests;
using StockTrader.APIHandler.Interfaces;
using StockTrader.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace StockTraderUnitTests.GatewayTests.PutTests
{
    [TestFixture]
    public class PutRequestHandlerTests
    {
        private IPutRequestHandler _putRequestHandler;
        private IPutRequestMaker _putRequestMaker;
        private IUrlBuilder _urlBuilder;
        private IStatusCodeHandler _statusCodeHandler;
        [SetUp]
        public void Setup()
        {
            _urlBuilder = A.Fake<IUrlBuilder>();
            _putRequestMaker = A.Fake<IPutRequestMaker>();
            _statusCodeHandler = A.Fake<IStatusCodeHandler>();
            _putRequestHandler = new PutRequestHandler(_urlBuilder, _putRequestMaker, _statusCodeHandler);
        }

        [Test]
        public async Task PutPurchaseOnStockCallsUrlBuilderAndRequestMakerSuccessfullyAndReturnsString()
        {
            //Arrange
            var tradeId = "12344";
            var purchasePrice = 12345;
            var accessToken = "ABC";
            A.CallTo(() => _urlBuilder.BuildPurchaseUrl(tradeId)).Returns("DaveUrl");
            A.CallTo(() => _putRequestMaker.PutPurchaseOnStock("DaveUrl", accessToken, purchasePrice))
                .Returns(HttpStatusCode.OK);
            A.CallTo(() => _statusCodeHandler.HandlePurchaseStockStatusCode(HttpStatusCode.OK)).Returns("Success");

            //Act
            var actual = await _putRequestHandler.PutPurchaseOnStock(tradeId, purchasePrice, accessToken);

            //Assert
            Assert.AreEqual("Success", actual);
        }

        [Test]
        public async Task PutPurchaseOnStockCallsUrlBuilderAndRequestMakerFailsAndReturnsString()
        {
            //Arrange
            var tradeId = "12344";
            var purchasePrice = 12345;
            var accessToken = "ABC";
            A.CallTo(() => _urlBuilder.BuildPurchaseUrl(tradeId)).Returns("DaveUrl");
            A.CallTo(() => _putRequestMaker.PutPurchaseOnStock("DaveUrl", accessToken, purchasePrice))
                .Returns(HttpStatusCode.Unauthorized);
            A.CallTo(() => _statusCodeHandler.HandlePurchaseStockStatusCode(HttpStatusCode.Unauthorized)).Returns("Expired");

            //Act
            var actual = await _putRequestHandler.PutPurchaseOnStock(tradeId, purchasePrice, accessToken);

            //Assert
            Assert.AreEqual("Expired", actual);
        }

        [Test]
        public async Task MoveStockToTradePileBuildsModelAndCallsPutMakerThenReturnsString()
        {
            //Arrange
            var accessToken = "Abc";
            var stockId = "123";
            var tradeId = "321";
            var url = "itemUrl";
            var expectedModel = new MoveStockBodyModel
            {
                ItemData = new List<MoveStockDataModel>
                {
                    new MoveStockDataModel
                    {
                        Pile = "trade",
                        StockId = "123",
                        tradeId = "321"
                    }
                }
            };

            A.CallTo(() => _urlBuilder.GetItemUrl()).Returns(url);
            A.CallTo(() => _putRequestMaker.MoveStockToTradePile(url, accessToken, A<MoveStockBodyModel>.Ignored))
                .Returns(HttpStatusCode.OK);

            //Act
            var actual = await _putRequestHandler.MoveStockToTradePile(tradeId, stockId, accessToken);

            //Assert
            actual.Should().Be(HttpStatusCode.OK);
        }
    }
}
