using FakeItEasy;
using StockTrader.APIHandler.HttpHandlers.PostRequests;
using StockTrader.APIHandler.Interfaces;
using StockTrader.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockTraderUnitTests.GatewayTests.PostTests
{
    [TestFixture]
    public class PostRequestHandlerTests
    {
        private IPostRequestHandler _postRequestHandler;
        private IPostRequestMaker _postRequestMaker;
        private IUrlBuilder _urlBuilder;
        private IStatusCodeHandler _statusCodeHandler;
        private string _token;
        private string _stockId;
        private int _startPrice;
        private int _binPrice;

        [SetUp]
        public void Setup()
        {
            _urlBuilder = A.Fake<IUrlBuilder>();
            _postRequestMaker = A.Fake<IPostRequestMaker>();
            _statusCodeHandler = A.Fake<IStatusCodeHandler>();
            _postRequestHandler = new PostRequestHandler(_postRequestMaker, _urlBuilder, _statusCodeHandler);
            _token = "ABC";
            _stockId = "12345";
            _startPrice = 1200;
            _binPrice = 1100;
        }

        [Test]
        public async Task SellStockCallsUrlBuilderThenRequestMakerReturns200ThenStatusCodeHandlerReturnsSellingAsync()
        {
            //Arrange
            
            A.CallTo(() => _urlBuilder.GetSymbolUrl()).Returns("Jeff");
            A.CallTo(() => _postRequestMaker.SellStock("Jeff", _token, A<SellStockModel>.Ignored)).Returns(HttpStatusCode.OK);
            A.CallTo(() => _statusCodeHandler.HandleSellingStatusCode(HttpStatusCode.OK)).Returns("Selling");

            //Act
            var actual = await _postRequestHandler.SellStock(_stockId, _token, _startPrice, _binPrice);

            //Assert
            Assert.AreEqual("Selling", actual);
            A.CallTo(() => _urlBuilder.GetSymbolUrl()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _postRequestMaker.SellStock("Jeff", _token, A<SellStockModel>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _statusCodeHandler.HandleSellingStatusCode(HttpStatusCode.OK)).MustHaveHappenedOnceExactly();
        }
    }
}
