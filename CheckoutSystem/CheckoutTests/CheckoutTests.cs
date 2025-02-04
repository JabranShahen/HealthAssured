using Microsoft.VisualStudio.TestTools.UnitTesting;
using Implementations.Services;
using Moq;
using CheckoutSystem.Abstractions.Entities;
using CheckoutSystem.Abstractions.Services;
using System.Collections.Generic;
using System.Linq;

namespace Implementations.Tests
{
    [TestClass]
    public class CheckoutTests
    {
        [TestMethod]
        public void Scan_AddsItemToCheckout()
        {
            // Arrange
            var promotionServiceMock = new Mock<IPromotionService>();
            var itemServiceMock = new Mock<IItemService>();
            var checkout = new Checkout(promotionServiceMock.Object, itemServiceMock.Object);
            var itemSKU = "A";
            var itemDetails = new Item { SKU = "A", UnitPrice = 50 }; // Mock item details from item service

            itemServiceMock.Setup(s => s.GetItem(It.IsAny<string>())).Returns(itemDetails);

            // Act
            checkout.Scan(itemSKU);

            // Assert
            var scannedItems = checkout.GetCheckoutItems();
            Assert.AreEqual(1, scannedItems.Count());
            Assert.AreEqual(itemDetails, scannedItems.First().Item);
            Assert.AreEqual(1, scannedItems.First().Quantity);
        }

        [TestMethod]
        public void CalculateTotalPrice_OneAOneB_NoPromotion()
        {
            // Arrange
            var promotionServiceMock = new Mock<IPromotionService>();
            var itemServiceMock = new Mock<IItemService>();

            // Mock items
            var itemA = new Item { SKU = "A", UnitPrice = 50 };
            var itemB = new Item { SKU = "B", UnitPrice = 30 };

            // Set up item service
            itemServiceMock.Setup(s => s.GetItem("A")).Returns(itemA);
            itemServiceMock.Setup(s => s.GetItem("B")).Returns(itemB);

            var checkout = new Checkout(promotionServiceMock.Object, itemServiceMock.Object);

            // Act
            checkout.Scan("A");
            checkout.Scan("B");

            var totalPrice = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(80, totalPrice); // 50 for A + 30 for B = 80
        }
    }
}
