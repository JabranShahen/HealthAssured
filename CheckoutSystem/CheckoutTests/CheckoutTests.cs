using Microsoft.VisualStudio.TestTools.UnitTesting;
using Implementations.Services;
using Moq;
using CheckoutSystem.Abstractions.Entities;
using CheckoutSystem.Abstractions.Services;
using System.Collections.Generic;

[TestClass]
public class CheckoutTests
{
    [TestMethod]
    public void ScanItem_AddsItemToCheckout()
    {
        // Arrange
        var promotionServiceMock = new Mock<IPromotionService>();
        var checkout = new Checkout(promotionServiceMock.Object);
        var item = new Item { SKU = "A", UnitPrice = 50 };

        // Act
        checkout.ScanItem(item);
        decimal totalPrice = checkout.CalculateTotalPrice();

        // Assert
        Assert.AreEqual(item.UnitPrice, totalPrice);
    }
}
