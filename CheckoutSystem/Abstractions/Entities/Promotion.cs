namespace CheckoutSystem.Abstractions.Entities
{
    public class Promotion
    {
        public string Name { get; set; }
        public string AssociatedItemSKU { get; set; } 
        public int Quantity { get; set; } 
        public decimal Price { get; set; }

    }
}
