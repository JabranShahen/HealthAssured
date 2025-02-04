using CheckoutSystem.Abstractions.Entities;
using CheckoutSystem.Abstractions.Services;
using System.Collections.Generic;
using System.Linq;

namespace Implementations.Services
{
    public class Checkout : ICheckout
    {
        private readonly IPromotionService _promotionService;
        private readonly IItemService _itemService;
        private readonly List<CheckoutItem> _scannedItems;

        public Checkout(IPromotionService promotionService, IItemService itemService)
        {
            _promotionService = promotionService;
            _itemService = itemService;
            _scannedItems = new List<CheckoutItem>();
        }

        public void Scan(string itemSKU)
        {
            var itemDetails = _itemService.GetItem(itemSKU);
            _scannedItems.Add(new CheckoutItem { Item = itemDetails, Quantity = 1 });
        }

        public decimal CalculateTotalPrice()
        {
            return _scannedItems.Sum(item => item.Item.UnitPrice);
        }

        public IEnumerable<CheckoutItem> GetCheckoutItems()
        {
            return _scannedItems;
        }
    }
}
