namespace CheckoutSystem
{
    // Entity representing an item in the supermarket
    public class Item
    {
        public string SKU { get; }       // Stock Keeping Unit
        public decimal UnitPrice { get; } // Unit price of the item

        // Constructor to initialize an item with SKU and unit price
        public Item(string sku, decimal unitPrice)
        {
            SKU = sku;
            UnitPrice = unitPrice;
        }

        // Additional methods or properties related to the item entity can be added here
    }
}
