using FakeItEasy;
using StockTrader.APIHandler;
using StockTrader.APIHandler.Interfaces;
using StockTrader.Models;
using StockTrader.Models.ModelBuilders;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;

namespace StockTraderUnitTests.GatewayTests
{

    [TestFixture]
    public class ApiGatewayTests
    {
        private IApiGateway _ApiGateway;
        private IGetRequestHandler _getRequestHandler;
        private IPurchaseViewModelBuilder _modelBuilder;
        private IPutRequestHandler _putRequestHandler;
        private IPostRequestHandler _postRequestHandler;
        private IDeleteHandler _deleteHandler;

        [SetUp]
        public void Setup()
        {
            _getRequestHandler = A.Fake<IGetRequestHandler>();
            _modelBuilder = A.Fake<IPurchaseViewModelBuilder>();
            _putRequestHandler = A.Fake<IPutRequestHandler>();
            _postRequestHandler = A.Fake<IPostRequestHandler>();
            _deleteHandler = A.Fake<IDeleteHandler>();
            _ApiGateway = new ApiGateway(_getRequestHandler, _modelBuilder, _putRequestHandler,
                _postRequestHandler, _deleteHandler);
        }

        [Test]
        public async Task FetchStocksCallsGetHandlerSuccessfullyAndReturnsListOfPurchaseViewModelAsync()
        {
            // Arrange
            var purchasePrice = 1400;
            var stockId = 12345;
            var accessToken = "ABC";
            var searchView = new List<StockSearchModel>
            {
                new StockSearchModel
                {
                    TimeRemaining=123,
                    TradeId="12345"
                },
                new StockSearchModel
                {
                    TimeRemaining=1234,
                    TradeId="12345789"
                }
            };
            var purchaseView = new List<PurchaseViewModel>
            {
                new PurchaseViewModel
                {
                    Pending=true
                },
                 new PurchaseViewModel
                {
                     Pending=true
                }
            };

            A.CallTo(() => _getRequestHandler.SearchForSpecificStock(12345, 1400, "ABC"))
                .Returns(purchaseView);
            //A.CallTo(() => _modelBuilder.ConvertSearchModelToPurchaseView(searchView)).Returns(buildView);

            // Act
            var actual = await _ApiGateway.FetchStocks(stockId, purchasePrice, accessToken);

            // Assert
            Assert.IsInstanceOf<List<PurchaseViewModel>>(actual);
            Assert.AreEqual(2, actual.Count);
        }

        [Test]
        public async Task PurchaseOnStockCallsRequestHandlerAndReturnsAPurchaseViewModelAsync()
        {
            //Arrange
            var purchasePrice = 1400;
            var tradeId = "12345";
            var accessToken = "ABC";
            var expected = "Success";
            A.CallTo(() => _putRequestHandler.PutPurchaseOnStock(tradeId, purchasePrice, accessToken))
                .Returns(expected);

            //Act
            var actual = await _ApiGateway.PurchaseOnStock(tradeId, purchasePrice, accessToken);

            //Assert
            actual.Should().BeEquivalentTo(expected);
            A.CallTo(() => _putRequestHandler.PutPurchaseOnStock(tradeId, purchasePrice, accessToken)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task GetSellStocksCallsGetHandlerAndReturnsListOfPurchaseViewModelAsync()
        {
            //Arrange
            var accessToken = "ABC";
            var initial = new SymbolSearchModel
            {
                SymbolInfo = new List<PurchaseViewModel>
                {
                    new PurchaseViewModel
                    {
                        Status="Pending",
                        PurchasePrice=1300,
                        Pending=true,
                        TimeRemaining=123,
                        TradeId="123"
                    },
                    new PurchaseViewModel
                    {
                        Status="Pending",
                        PurchasePrice=1300,
                        Pending=true,
                        TimeRemaining=-1,
                        TradeId="123"
                    }
                }
            };

            var expected = new SymbolSearchModel
            {
                SymbolInfo = new List<PurchaseViewModel>
                {
                    new PurchaseViewModel
                    {
                        Status="Pending",
                        PurchasePrice=1300,
                        Pending=true,
                        TimeRemaining=123,
                        TradeId="123"
                    },
                    new PurchaseViewModel
                    {
                        Status="Pending",
                        PurchasePrice=1300,
                        Pending=false,
                        TimeRemaining=-1,
                        TradeId="123"
                    }
                }
            };

            A.CallTo(() => _getRequestHandler.GetSellStocks(accessToken))
                .Returns(initial);

            A.CallTo(() => _modelBuilder.PopulateDefaultFieldsOfPurchaseViews(initial.SymbolInfo))
                .Returns(expected.SymbolInfo);

            //Act
            var actual = await _ApiGateway.GetSellStocks(accessToken);

            //Assert
            A.CallTo(() => _getRequestHandler.GetSellStocks(accessToken)).MustHaveHappenedOnceExactly();

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task SellStockCallsPutHandlerReturns200ThenCallsPostHandlerAndReturnsSellingAsync()
        {
            //Arrange
            var token = "ABC";
            var startPrice = 1100;
            var binPrice = 1200;
            var stockId = "12345";
            var tradeId = "54321";

            A.CallTo(() => _putRequestHandler.MoveStockToTradePile(tradeId, stockId, token))
                .Returns(HttpStatusCode.OK);
            A.CallTo(() => _postRequestHandler.SellStock(stockId, token, startPrice, binPrice))
                .Returns("Selling");

            //Act
            var actual = await _ApiGateway.SellStock(tradeId, stockId, token, startPrice, binPrice);

            //Assert
            Assert.AreEqual("Selling", actual);
            A.CallTo(() => _putRequestHandler.MoveStockToTradePile(tradeId, stockId, token)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _postRequestHandler.SellStock(stockId, token, startPrice, binPrice)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task SellStockCallsPutHandlerReturns400ErrorAsync()
        {
            //Arrange
            var token = "ABC";
            var startPrice = 1100;
            var binPrice = 1200;
            var stockId = "12345";
            var tradeId = "54321";

            A.CallTo(() => _putRequestHandler.MoveStockToTradePile(tradeId, stockId, token))
                .Returns(HttpStatusCode.BadRequest);

            //Act
            var actual = await _ApiGateway.SellStock(tradeId, stockId, token, startPrice, binPrice);

            //Assert
            Assert.AreEqual("Error", actual);
            A.CallTo(() => _putRequestHandler.MoveStockToTradePile(tradeId, stockId, token)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _postRequestHandler.SellStock(stockId, token, startPrice, binPrice)).MustNotHaveHappened();
        }

        [Test]
        public async Task CheckTokenCallsHttpHandlerAndReturnsAStringAsync()
        {
            //Arrange
            var token = "ABC";
            A.CallTo(() => _getRequestHandler.CheckToken(token)).Returns("Valid");

            //Act
            var actual = await _ApiGateway.CheckToken(token);

            //Assert
            Assert.AreEqual("Valid", actual);
        }

        [Test]
        public void ClearExpiredStocksCallsDeleteHandlerAndDoesNotThrowError()
        {
            //Arrange
            var token = "ABC";
            var stocks = new List<PurchaseViewModel>();

            //Act

            //Assert
            Assert.DoesNotThrow(() => _ApiGateway.ClearExpiredStocks(token, stocks));
        }
    }
}
