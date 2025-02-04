namespace CheckoutSystem.Abstractions.Entites
{

    public class Item
    {
        public string SKU { get; }       
        public decimal UnitPrice { get; }

        
        public Item(string sku, decimal unitPrice)
        {
            SKU = sku;
            UnitPrice = unitPrice;
        }        
    }
}
