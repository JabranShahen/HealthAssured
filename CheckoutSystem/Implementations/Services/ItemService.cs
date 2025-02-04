
using CheckoutSystem.Abstractions.Services;
using CheckoutSystem.Abstractions.Entites;
using System.Collections.Generic;

namespace Implementations.Services
{
    public class ItemService : IItemService
    {

        void IItemService.AddItem(Item item)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<Item> IItemService.GetAllItems()
        {
            throw new System.NotImplementedException();
        }

        Item IItemService.GetItem(string sku)
        {
            throw new System.NotImplementedException();
        }
    }
}
