using CheckoutSystem.Abstractions.Entites;
using CheckoutSystem.Abstractions.Services;

namespace Implementations.Services
{
    public class Checkout : ICheckout
    {
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
