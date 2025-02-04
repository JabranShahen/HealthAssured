using System.Collections.Generic;
using System.Linq;
using CheckoutSystem.Abstractions.Entities;
using CheckoutSystem.Abstractions.Services;

namespace Implementations.Services
{
    public class ItemService : IItemService
    {
        private readonly List<Item> _items;

        public ItemService()
        {
            _items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public Item GetItem(string sku)
        {
            return _items.FirstOrDefault(item => item.SKU == sku);
        }

    }
}
