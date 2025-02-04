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
            var existingItem = _scannedItems.FirstOrDefault(i => i.Item.SKU == itemSKU);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                _scannedItems.Add(new CheckoutItem { Item = itemDetails, Quantity = 1 });
            }
        }

        public decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;

            foreach (var itemGroup in _scannedItems.GroupBy(item => item.Item.SKU))
            {
                var item = itemGroup.First().Item;
                var quantity = itemGroup.Sum(item => item.Quantity);

                // Check if there are any promotions for this item
                var promotions = _promotionService.GetPromotionsForItem(item.SKU);
                if (promotions.Any())
                {
                    // Apply promotions
                    foreach (var promotion in promotions)
                    {
                        if (quantity >= promotion.Quantity)
                        {
                            totalPrice += promotion.Price * (quantity / promotion.Quantity) + item.UnitPrice * (quantity % promotion.Quantity);
                            break; // Only apply the first matching promotion
                        }
                    }
                }
                else
                {
                    // No promotions, calculate price without discount
                    totalPrice += item.UnitPrice * quantity;
                }
            }

            return totalPrice;
        }

        public IEnumerable<CheckoutItem> GetCheckoutItems()
        {
            return _scannedItems;
        }
    }
}
