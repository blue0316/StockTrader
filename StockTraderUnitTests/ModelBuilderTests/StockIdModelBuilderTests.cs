using StockTrader.Models;
using StockTrader.Models.ModelBuilders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockTraderUnitTests.ModelBuilderTests
{
    [TestFixture]
    public class StockIdModelBuilderTests
    {
        private IStockIdModelBuilder _stockIdModelBuilder;
        [SetUp]
        public void Setup()
        {
            _stockIdModelBuilder = new StockIdModelBuilder();
        }
        
        [Test]
        public void ReadStockStoreReturnsListOfStockIdModel()
        {
            var actual = _stockIdModelBuilder.ReadStoredStocks();

            Assert.IsInstanceOf<List<StockIdModel>>(actual);
        }
    }
}
