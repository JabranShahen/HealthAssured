using System.Collections.Generic;

namespace CheckoutSystem.Services
{
    public interface IItemService
    {
        void AddItem(Item item);
        Item GetItem(string sku);
        IEnumerable<Item> GetAllItems();
    }
}
