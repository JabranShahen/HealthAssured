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
        public void CalculateTotalPrice_NoItems()
        {
            // Arrange
            var promotionServiceMock = new Mock<IPromotionService>();
            var itemServiceMock = new Mock<IItemService>();
            var checkout = new Checkout(promotionServiceMock.Object, itemServiceMock.Object);

            // Act
            var totalPrice = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(0, totalPrice);
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

        [TestMethod]
        public void CalculateTotalPrice_SingleItem_WithPromotion()
        {
            // Arrange
            var promotionServiceMock = new Mock<IPromotionService>();
            var itemServiceMock = new Mock<IItemService>();

            // Mock item
            var itemA = new Item { SKU = "A", UnitPrice = 50 };
            itemServiceMock.Setup(s => s.GetItem("A")).Returns(itemA);

            // Mock promotion
            var promotionA = new Promotion { Name = "3 for 130", AssociatedItemSKU = "A", Quantity = 3, Price = 130 };
            promotionServiceMock.Setup(s => s.GetPromotionsForItem("A")).Returns(new List<Promotion> { promotionA });

            var checkout = new Checkout(promotionServiceMock.Object, itemServiceMock.Object);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            var totalPrice = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(130, totalPrice);
        }

        [TestMethod]
        public void CalculateTotalPrice_MultipleItems_WithPromotion()
        {
            // Arrange
            var promotionServiceMock = new Mock<IPromotionService>();
            var itemServiceMock = new Mock<IItemService>();

            var itemA = new Item { SKU = "A", UnitPrice = 50 };
            var itemB = new Item { SKU = "B", UnitPrice = 30 };

            var promotions = new List<Promotion>
            {
                new Promotion { Name = "Buy 3 for 130", AssociatedItemSKU = "A", Quantity = 3, Price = 130 }
            };

            itemServiceMock.Setup(s => s.GetItem("A")).Returns(itemA);
            itemServiceMock.Setup(s => s.GetItem("B")).Returns(itemB);
            promotionServiceMock.Setup(p => p.GetPromotionsForItem("A")).Returns(promotions);

            var checkout = new Checkout(promotionServiceMock.Object, itemServiceMock.Object);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A"); // Should apply promotion
            checkout.Scan("B");

            var totalPrice = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(160, totalPrice); // Promotion price 130 for 3 A's and 30 for 1 B
        }

        [TestMethod]
        public void CalculateTotalPrice_MixedItems_WithPromotion()
        {
            // Arrange
            var promotionServiceMock = new Mock<IPromotionService>();
            var itemServiceMock = new Mock<IItemService>();

            var itemA = new Item { SKU = "A", UnitPrice = 50 };
            var itemB = new Item { SKU = "B", UnitPrice = 30 };
            var itemC = new Item { SKU = "C", UnitPrice = 20 };

            var promotions = new List<Promotion>
            {
                new Promotion { Name = "Buy 3 for 130", AssociatedItemSKU = "A", Quantity = 3, Price = 130 }
            };

            itemServiceMock.Setup(s => s.GetItem("A")).Returns(itemA);
            itemServiceMock.Setup(s => s.GetItem("B")).Returns(itemB);
            itemServiceMock.Setup(s => s.GetItem("C")).Returns(itemC);
            promotionServiceMock.Setup(p => p.GetPromotionsForItem("A")).Returns(promotions);

            var checkout = new Checkout(promotionServiceMock.Object, itemServiceMock.Object);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A"); // Should apply promotion
            checkout.Scan("B");
            checkout.Scan("C");

            var totalPrice = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(180, totalPrice); // Promotion price 130 for 3 A's, 30 for 1 B, and 20 for 1 C
        }

        [TestMethod]
        public void GetCheckoutItems_EmptyCheckout()
        {
            // Arrange
            var promotionServiceMock = new Mock<IPromotionService>();
            var itemServiceMock = new Mock<IItemService>();
            var checkout = new Checkout(promotionServiceMock.Object, itemServiceMock.Object);

            // Act
            var scannedItems = checkout.GetCheckoutItems();

            // Assert
            Assert.AreEqual(0, scannedItems.Count());
        }

        [TestMethod]
        public void GetCheckoutItems_NonEmptyCheckout()
        {
            // Arrange
            var promotionServiceMock = new Mock<IPromotionService>();
            var itemServiceMock = new Mock<IItemService>();

            var itemA = new Item { SKU = "A", UnitPrice = 50 };

            itemServiceMock.Setup(s => s.GetItem("A")).Returns(itemA);

            var checkout = new Checkout(promotionServiceMock.Object, itemServiceMock.Object);

            // Act
            checkout.Scan("A");
            var scannedItems = checkout.GetCheckoutItems();

            // Assert
            Assert.AreEqual(1, scannedItems.Count());
            Assert.AreEqual(itemA, scannedItems.First().Item);
            Assert.AreEqual(1, scannedItems.First().Quantity);
        }

        [TestMethod]
        public void GetCheckoutItems_CorrectQuantity()
        {
            // Arrange
            var promotionServiceMock = new Mock<IPromotionService>();
            var itemServiceMock = new Mock<IItemService>();

            var itemA = new Item { SKU = "A", UnitPrice = 50 };

            itemServiceMock.Setup(s => s.GetItem("A")).Returns(itemA);

            var checkout = new Checkout(promotionServiceMock.Object, itemServiceMock.Object);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            var scannedItems = checkout.GetCheckoutItems();

            // Assert
            Assert.AreEqual(1, scannedItems.Count());
            Assert.AreEqual(itemA, scannedItems.First().Item);
            Assert.AreEqual(2, scannedItems.First().Quantity);
        }

        [TestMethod]
        public void CalculateTotalPrice_PromotionAppliedToCorrectQuantity()
        {
            // Arrange
            var promotionServiceMock = new Mock<IPromotionService>();
            var itemServiceMock = new Mock<IItemService>();

            var promotion = new Promotion { Name = "Promo1", AssociatedItemSKU = "A", Quantity = 3, Price = 130 };
            promotionServiceMock.Setup(s => s.GetPromotionsForItem("A")).Returns(new List<Promotion> { promotion });

            var itemA = new Item { SKU = "A", UnitPrice = 50 };
            itemServiceMock.Setup(s => s.GetItem("A")).Returns(itemA);

            var checkout = new Checkout(promotionServiceMock.Object, itemServiceMock.Object);

            // Act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            var totalPrice = checkout.CalculateTotalPrice();

            // Assert
            Assert.AreEqual(180, totalPrice); // Promotion price for 3 items + 2 normal prices for 2 items
        }
    }
}
