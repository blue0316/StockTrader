using StockTrader.Models;
using StockTrader.Models.ModelBuilders;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace StockTraderUnitTests.ModelBuilderTests
{
    [TestFixture]
    public class PurchaseViewModelBuilderTests
    {
        private IPurchaseViewModelBuilder _purchaseViewModelBuilder;
        [SetUp]
        public void Setup()
        {
            _purchaseViewModelBuilder = new PurchaseViewModelBuilder();
        }

        [Test]
        public void ConvertSearchModelToPurchaseViewTakesSearchModelAndReturnsListOfPurchaseViewModel()
        {
            // Arrange
            var initial = new List<StockSearchModel>
            {
                new StockSearchModel
                {
                    TimeRemaining=123,
                    TradeId="12345"
                }
            };

            var expected = new List<PurchaseViewModel>
            {
                new PurchaseViewModel
                {
                    TimeRemaining=123,
                    TradeId="12345",
                    Pending=true,
                    Status="Pending"
                }
            };

            // Act
            var actual = _purchaseViewModelBuilder.ConvertSearchModelToPurchaseView(initial);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ConverSearchModelToPurchaseViewWhereEmptyListReturnsOneExpectedItem()
        {
            // Arrange
            var initial = new List<StockSearchModel>();
            var expected = new List<PurchaseViewModel>
            {
                new PurchaseViewModel
                {
                    Status="Error",
                    Pending=false
                }
            };

            // Act
            var actual = _purchaseViewModelBuilder.ConvertSearchModelToPurchaseView(initial);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void PopulateDefaultFieldsTakesListOfPurchaseViewAndReturnsCorrectModel()
        {
            //Arrange
            var initial = new List<PurchaseViewModel>
            {
                new PurchaseViewModel
                {
                    TimeRemaining=-1,
                    Status = "Outpurchase",
                    Pending = true
                },
                new PurchaseViewModel
                {
                    TimeRemaining=10,
                    Status="Winning",
                    Pending = true
                }
            };
            var expected = new List<PurchaseViewModel>
            {
                new PurchaseViewModel
                {
                    TimeRemaining=-1,
                    Status = "Outpurchase",
                    Pending = false
                },
                new PurchaseViewModel
                {
                    TimeRemaining=10,
                    Status="Winning",
                    Pending = true
                }
            };

            //Act
            var actual = _purchaseViewModelBuilder.PopulateDefaultFieldsOfPurchaseViews(initial);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
