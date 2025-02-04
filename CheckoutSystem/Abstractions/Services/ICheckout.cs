using CheckoutSystem.Abstractions.Entities;

namespace CheckoutSystem.Abstractions.Services
{
    public interface ICheckout
    {
        void ScanItem(Item item);
        decimal CalculateTotalPrice();
    }
}
