namespace CheckoutSystem.Abstractions.Entites
{
    public class Promotion
    {
        public string Name { get; }
        public string AssociatedItemSKU { get; } 
        public int Quantity { get; } 
        public decimal Price { get; }

        public Promotion(string name, string associatedItemSKU, int quantity, decimal price)
        {
            Name = name;
            AssociatedItemSKU = associatedItemSKU;
            Quantity = quantity;
            Price = price;
        }
    }
}
