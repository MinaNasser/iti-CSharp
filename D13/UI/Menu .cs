using System;
using System.Collections.Generic;
using Models;

namespace UI
{
    public class Menu
    {
        private List<Customer> customers = new List<Customer>();
        private List<Order> orders = new List<Order>();
        private Inventory inventory = new Inventory();

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Main Menu =====");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Add Item to Inventory");
                Console.WriteLine("3. Create Order");
                Console.WriteLine("4. View Orders");
                Console.WriteLine("5. View Inventory");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddCustomer();
                        break;
                    case "2":
                        AddItemToInventory();
                        break;
                    case "3":
                        CreateOrder();
                        break;
                    case "4":
                        ViewOrders();
                        break;
                    case "5":
                        inventory.ViewStock();
                        Console.ReadLine();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice! Press Enter to try again...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void AddCustomer()
        {
            Console.Clear();
            Console.Write("Enter Customer Name: ");
            string name = Console.ReadLine();

            Customer customer = new Customer
            {
                Id = customers.Count + 1,
                Name = name
            };

            customers.Add(customer);
            Console.WriteLine("Customer added successfully!");
            Console.ReadLine();
        }

        private void AddItemToInventory()
        {
            Console.Clear();
            Console.Write("Enter Item Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Item item = new Item
            {
                Id = inventory.GetStock().Count + 1,
                Name = name,
                Description = description
            };

            inventory.AddItem(item, quantity);
            Console.WriteLine("Item added to inventory!");
            Console.ReadLine();
        }

        private void CreateOrder()
        {
            Console.Clear();
            if (customers.Count == 0)
            {
                Console.WriteLine("No customers found! Add a customer first.");
                Console.ReadLine();
                return;
            }

            if (inventory.GetStock().Count == 0)
            {
                Console.WriteLine("No items in inventory! Add items first.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Select a customer:");
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {customers[i].Name}");
            }

            Console.Write("Enter customer number: ");
            int customerIndex = int.Parse(Console.ReadLine()) - 1;
            if (customerIndex < 0 || customerIndex >= customers.Count)
            {
                Console.WriteLine("Invalid customer! Press Enter to return...");
                Console.ReadLine();
                return;
            }

            Order order = new Order
            {
                Id = orders.Count + 1,
                Date = DateTime.Now,
                Customer = customers[customerIndex],
                Items = new List<OrderItem>()
            };

            Console.Clear();
            Console.WriteLine("Order created! Now, add items to the order.");

            while (true)
            {
                Console.WriteLine("Select an item from inventory:");
                var stock = inventory.GetStock();
                for (int i = 0; i < stock.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {stock[i].Item.Name} | Available: {stock[i].Quantity}");
                }

                Console.Write("Enter item number (or 0 to finish): ");
                int itemIndex = int.Parse(Console.ReadLine()) - 1;

                if (itemIndex == -1) break;

                if (itemIndex < 0 || itemIndex >= stock.Count)
                {
                    Console.WriteLine("Invalid item! Try again...");
                    continue;
                }

                Console.Write("Enter quantity: ");
                int qty = int.Parse(Console.ReadLine());

                if (!inventory.DecreaseItems(stock[itemIndex].Item.Id, qty))
                {
                    Console.WriteLine("Not enough stock! Try again...");
                    continue;
                }

                order.Items.Add(new OrderItem
                {
                    Id = order.Items.Count + 1,
                    Item = stock[itemIndex].Item,
                    Qty = qty,
                    Price = 10.0m
                });

                Console.WriteLine("Item added to order!");
                Console.Write("Do you want to add another item? (y/n): ");
                string continueAdding = Console.ReadLine().ToLower();
                if (continueAdding != "y")
                    break;
            }

            orders.Add(order);
            Console.WriteLine("Order created successfully!");
            Invois invoice = new Invois();
            invoice.Print(order);
        }

        private void ViewOrders()
        {
            Console.Clear();
            if (orders.Count == 0)
            {
                Console.WriteLine("No orders found.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Order List:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.Id}, Customer: {order.Customer.Name}, Date: {order.Date}");
            }

            Console.WriteLine("Press Enter to go back...");
            Console.ReadLine();
        }
    }
}
