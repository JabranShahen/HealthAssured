using System.Collections.Generic;

namespace CheckoutSystem.Services
{
    public interface IPromotionService
    {
        void AddPromotion(Promotion promotion);
        IEnumerable<Promotion> GetPromotionsForItem(string sku);
    }
}
