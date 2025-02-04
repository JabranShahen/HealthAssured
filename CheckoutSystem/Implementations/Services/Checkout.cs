using CheckoutSystem.Abstractions.Entities;
using CheckoutSystem.Abstractions.Services;

namespace Implementations.Services
{
    public class Checkout : ICheckout
    {
        private readonly IPromotionService _promotionService;

        public Checkout(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        public void ScanItem(Item item)
        {
            throw new System.NotImplementedException();
        }

        public decimal CalculateTotalPrice()
        {
            // Implementation to be added
            return 0; // Placeholder return value
        }
    }
}
