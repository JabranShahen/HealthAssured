using CheckoutSystem.Abstractions.Entities;
using System.Collections.Generic;

namespace CheckoutSystem.Abstractions.Services
{
    public interface IItemService
    {
        void AddItem(Item item);
        Item GetItem(string sku);
    }
}
