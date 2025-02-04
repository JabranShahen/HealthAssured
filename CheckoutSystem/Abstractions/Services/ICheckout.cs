using System.Collections.Generic;
using CheckoutSystem;

namespace CheckoutSystem.Services
{
    public interface ICheckout
    {
        void ScanItem(Item item);
        decimal CalculateTotalPrice();
    }
}
