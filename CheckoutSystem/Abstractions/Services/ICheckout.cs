using CheckoutSystem.Abstractions.Entities;
using System.Collections.Generic;

namespace CheckoutSystem.Abstractions.Services
{
    public interface ICheckout
    {
        void Scan(string itemSKU);
        decimal CalculateTotalPrice();
        IEnumerable<CheckoutItem> GetCheckoutItems();
    }
}
