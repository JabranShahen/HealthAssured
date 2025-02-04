namespace CheckoutSystem.Abstractions.Entities
{
    public class CheckoutItem
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
