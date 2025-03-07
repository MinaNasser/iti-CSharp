using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class Inventory
    {
        private List<ItemStock> stock = new List<ItemStock>();

        public void AddItem(Item item, int quantity)
        {
            var existingItem = stock.FirstOrDefault(s => s.Item.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                stock.Add(new ItemStock { Item = item, Quantity = quantity });
            }
            Console.WriteLine($" Added {quantity} of {item.Name} to inventory.");
        }

        public bool DecreaseItems(int itemId, int quantity)
        {
            var itemStock = stock.FirstOrDefault(s => s.Item.Id == itemId);
            if (itemStock == null || itemStock.Quantity < quantity)
            {
                Console.WriteLine($" Not enough stock for item {itemStock?.Item.Name ?? "Unknown"}!");
                return false;
            }

            itemStock.Quantity -= quantity;
            Console.WriteLine($" {quantity} of {itemStock.Item.Name} removed from inventory.");
            return true;
        }

        public void ViewStock()
        {
            Console.Clear();
            if (stock.Count == 0)
            {
                Console.WriteLine(" No items in inventory.");
                return;
            }

            Console.WriteLine(" Inventory Stock:");
            foreach (var item in stock)
            {
                Console.WriteLine($"- {item.Item.Name} | Available: {item.Quantity}");
            }
        }

        public List<ItemStock> GetStock() => stock;
    }

    public class ItemStock
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}
