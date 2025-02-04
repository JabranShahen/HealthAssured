using Microsoft.VisualStudio.TestTools.UnitTesting;
using Implementations.Services;
using CheckoutSystem.Abstractions.Entities;

namespace UnitTests.Services
{
    [TestClass]
    public class ItemServiceTests
    {
        [TestMethod]
        public void AddItem_ItemAddedSuccessfully()
        {
            // Arrange
            var itemService = new ItemService();
            var item = new Item { SKU = "A", UnitPrice = 50 };

            // Act
            itemService.AddItem(item);

            // Assert
            var retrievedItem = itemService.GetItem("A");
            Assert.AreEqual(item, retrievedItem);
        }

    }
}
