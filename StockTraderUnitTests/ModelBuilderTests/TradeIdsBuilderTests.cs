using StockTrader.Models;
using StockTrader.Models.ModelBuilders;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockTraderUnitTests.ModelBuilderTests
{
    [TestFixture]
    public class TradeIdsBuilderTests
    {
        private ITradeIdsBuilder _tradeIdsBuilder;

        [SetUp]
        public void setup()
        {
            _tradeIdsBuilder = new TradeIdsBuilder();
        }

        [Test]
        public void GetTradeIdsTakesStocksAndReturnsStringWithIds()
        {
            //Arrange
            var stocks = new List<PurchaseViewModel>
            {
                new PurchaseViewModel
                {
                    TradeId = "12345",
                    Status="outpurchase"
                },
                new PurchaseViewModel
                {
                    TradeId = "54321",
                    Status = "outpurchase"
                },
                new PurchaseViewModel
                {
                    TradeId = "9876",
                    Status = "highest"
                }

            };
            var expected = new TradeIdsModel
            {
                TradeIds = "12345,54321"
            };

            //Act
            var actual = _tradeIdsBuilder.GetTradeIds(stocks);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
