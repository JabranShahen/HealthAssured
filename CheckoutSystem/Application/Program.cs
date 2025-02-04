using System;
using CheckoutSystem.Abstractions.Entities;
using Implementations.Services; // Assuming your implementation classes are in this namespace

namespace CheckoutSystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize services
            var itemService = new ItemService(); // Your item service implementation
            var promotionService = new PromotionService(); // Your promotion service implementation

            // Add some items to the item service
            itemService.AddItem(new Item { SKU = "A", UnitPrice = 50 });
            itemService.AddItem(new Item { SKU = "B", UnitPrice = 30 });
            itemService.AddItem(new Item { SKU = "C", UnitPrice = 40 });
            // Add promotions
            promotionService.AddPromotion(new Promotion { Name = "3 for 130", AssociatedItemSKU = "A", Quantity = 3, Price = 130 });
            promotionService.AddPromotion(new Promotion { Name = "2 for 45", AssociatedItemSKU = "B", Quantity = 2, Price = 45 });

            // Create a checkout instance
            var checkout = new Checkout(promotionService, itemService);

            // Scan some items
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("B");
            checkout.Scan("C"); // Not on promotion

            // Calculate total price
            var totalPrice = checkout.CalculateTotalPrice();

            // Display the total price
            Console.WriteLine($"Total Price: {totalPrice}");
            Console.ReadLine();

        }
    }
}
