using FakeItEasy;
using StockTrader.APIHandler.HttpHandlers.GetRequests;
using StockTrader.APIHandler.Interfaces;
using StockTrader.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net;

namespace StockTraderUnitTests.GatewayTests.GetTests
{

    [TestFixture]
    public class GetRequestHandlerTests
    {
        private IGetRequestHandler _getRequestHandler;
        private IGetRequestMaker _getRequestMaker;
        private IUrlBuilder _urlBuilder;
        private IStatusCodeHandler _statusCodeHandler;

        [SetUp]
        public void Setup()
        {
            _getRequestMaker = A.Fake<IGetRequestMaker>();
            _urlBuilder = A.Fake<IUrlBuilder>();
            _statusCodeHandler = A.Fake<IStatusCodeHandler>();

            _getRequestHandler = new GetRequestHandler(_getRequestMaker, _urlBuilder, _statusCodeHandler);
        }

        [Test]
        public async Task SearchForSpecificStockBuildUrlCallsRequestMakerAndReturnsListSearchViewAsync()
        {
            // Arrange
            var purchasePrice = 1400;
            var stockId = 12345;
            var accessToken = "ABC";
            var expectedStockSearch = new List<PurchaseViewModel>();
            var expected = new SymbolSearchModel
            {
                SymbolInfo = expectedStockSearch
            };
            A.CallTo(() => _urlBuilder.BuildSearchUrl(12345, 1400)).Returns("Dave");
            A.CallTo(() => _getRequestMaker.MakeGetRequest("Dave", accessToken))
                .Returns(JsonConvert.SerializeObject(expected));

            // Act
            var actual = await _getRequestHandler.SearchForSpecificStock(stockId, purchasePrice, accessToken);

            // Assert
            Assert.IsInstanceOf<List<PurchaseViewModel>>(actual);
            A.CallTo(() => _urlBuilder.BuildSearchUrl(12345, 1400)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _getRequestMaker.MakeGetRequest("Dave", accessToken)).MustHaveHappenedOnceExactly();
        }
        [Test]
        public async Task SearchForSpecificStockWhereExpiredTokenThrowsExpectedErrorAndReturnsExpectedEmptyListAsync()
        {
            // Arrange
            var purchasePrice = 1400;
            var stockId = 12345;
            var accessToken = "ABC";
            var expectedStockSearch = new List<PurchaseViewModel>();
            var expected = new SymbolSearchModel
            {
                SymbolInfo = expectedStockSearch
            };
            A.CallTo(() => _urlBuilder.BuildSearchUrl(12345, 1400)).Returns("Dave");
            A.CallTo(() => _getRequestMaker.MakeGetRequest("Dave", accessToken)).Throws(new HttpRequestException());

            // Act
            var actual = await _getRequestHandler.SearchForSpecificStock(stockId, purchasePrice, accessToken);

            // Assert
            Assert.IsInstanceOf<List<PurchaseViewModel>>(actual);
            A.CallTo(() => _urlBuilder.BuildSearchUrl(12345, 1400)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _getRequestMaker.MakeGetRequest("Dave", accessToken)).MustHaveHappenedOnceExactly();
            Assert.AreEqual(0, actual.Count);
        }

        [Test]
        public async Task GetSellStocksGetsUrlMakesRequestThenReturnsListPurchaseViewModel()
        {
            //Arrange
            var accessToken = "ABC";
            var expectedStocks = new List<PurchaseViewModel>
            {
                new PurchaseViewModel
                {
                    Status="Pending",
                    PurchasePrice=1400,
                    Pending=true,
                    TimeRemaining=123,
                    TradeId="12234"
                }
            };

            var expected = new SymbolSearchModel
            {
                SymbolInfo = expectedStocks
            };

            A.CallTo(() => _urlBuilder.GetSellStocksUrl()).Returns("TargetsUrl");
            A.CallTo(() => _getRequestMaker.MakeGetRequest("TargetsUrl", accessToken))
                .Returns(JsonConvert.SerializeObject(expected));

            //Act
            var actual = await _getRequestHandler.GetSellStocks(accessToken);

            //Assert
            actual.SymbolInfo.Should().BeEquivalentTo(expectedStocks);
            A.CallTo(() => _getRequestMaker.MakeGetRequest("TargetsUrl", accessToken)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task CheckUrlMakesRequestWhichReturnsSuccessfullyThenReturnsValid()
        {
            //Arrange
            var token = "ABC";
            var url = "TokenUrl";
            var expectedCode = HttpStatusCode.OK;
            A.CallTo(() => _urlBuilder.GetCheckTokenUrl()).Returns(url);
            A.CallTo(() => _getRequestMaker.MakeGetRequestStatusCode(url, token)).Returns(expectedCode);
            A.CallTo(() => _statusCodeHandler.HandleTokenCheckStatusCode(expectedCode)).Returns("Valid");

            //Act
            var actual = await _getRequestHandler.CheckToken(token);

            //Assert
            Assert.AreEqual("Valid", actual);
        }

        [Test]
        public async Task CheckUrlMakesRequestWhichReturnsNOTSuccesfulThenReturnsInvalid()
        {
            //Arrange
            var token = "ABC";
            var url = "TokenUrl";
            var expectedCode = HttpStatusCode.Unauthorized;
            A.CallTo(() => _urlBuilder.GetCheckTokenUrl()).Returns(url);
            A.CallTo(() => _getRequestMaker.MakeGetRequestStatusCode(url, token)).Returns(expectedCode);
            A.CallTo(() => _statusCodeHandler.HandleTokenCheckStatusCode(expectedCode)).Returns("Invalid");

            //Act
            var actual = await _getRequestHandler.CheckToken(token);

            //Assert
            Assert.AreEqual("Invalid", actual);
        }
    }
}
