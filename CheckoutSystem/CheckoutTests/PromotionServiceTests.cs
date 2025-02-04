using Microsoft.VisualStudio.TestTools.UnitTesting;
using Implementations.Services; // Assuming PromotionService is in this namespace
using CheckoutSystem.Abstractions.Entities;
using System.Collections.Generic;

namespace UnitTests.Services
{
    [TestClass]
    public class PromotionServiceTests
    {
        [TestMethod]
        public void AddPromotion_PromotionAddedSuccessfully()
        {
            // Arrange
            var promotionService = new PromotionService();
            var promotion = new Promotion { Name = "Promo1", AssociatedItemSKU = "A", Quantity = 3, Price = 130 };

            // Act
            promotionService.AddPromotion(promotion);

            // Assert
            var retrievedPromotion = promotionService.GetPromotionsForItem("A");
            CollectionAssert.Contains((System.Collections.ICollection?)retrievedPromotion, promotion);
        }
       
    }
}
