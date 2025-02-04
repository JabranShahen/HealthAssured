using CheckoutSystem.Abstractions.Entites;

namespace CheckoutSystem.Abstractions.Services
{
    public interface ICheckout
    {
        void ScanItem(Item item);
        decimal CalculateTotalPrice();
    }
}
