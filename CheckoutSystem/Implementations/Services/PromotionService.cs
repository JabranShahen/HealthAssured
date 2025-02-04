using CheckoutSystem.Abstractions.Entities;
using CheckoutSystem.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Implementations.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly List<Promotion> _promotions;

        public PromotionService()
        {
            _promotions = new List<Promotion>();
        }

        public void AddPromotion(Promotion promotion)
        {
            _promotions.Add(promotion);
        }

        public IEnumerable<Promotion> GetPromotionsForItem(string sku)
        {
            return _promotions.Where(p => p.AssociatedItemSKU == sku);
        }
    }
}
