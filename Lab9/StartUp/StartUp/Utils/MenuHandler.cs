using System;
using StartUp.Managers;
using StartUp.Models;

namespace StartUp
{
    public class MenuHandler
    {
        private CustomerManager customerManager;
        private OrderManager orderManager;

        public MenuHandler(CustomerManager customerManager, OrderManager orderManager)
        {
            this.customerManager = customerManager;
            this.orderManager = orderManager;
        }

        public void ShowMenu()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Show all customers");
                Console.WriteLine("2. Show all orders");
                Console.WriteLine("3. Add a new customer");
                Console.WriteLine("4. Add a new order");
                Console.WriteLine("5. Exit");
                Console.Write("Your choice: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ShowCustomers();
                        break;
                    case 2:
                        ShowOrders();
                        break;
                    case 3:
                        AddCustomer();
                        break;
                    case 4:
                        AddOrder();
                        break;
                    case 5:
                        Console.WriteLine("Thank you for using the program!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

            } while (choice != 5);
        }

        private void ShowCustomers()
        {
            Console.WriteLine("\nCustomer List:");
            foreach (var customer in customerManager.Get())
            {
                Console.WriteLine(customer);
            }
        }

        private void ShowOrders()
        {
            Console.WriteLine("\nOrder List:");
            foreach (var order in orderManager.Get())
            {
                Console.WriteLine(order);
            }
        }

        private void AddCustomer()
        {
            Console.Write("\nEnter customer name: ");
            string name = Console.ReadLine();
            int id = customerManager.Get().Count + 1;
            customerManager.Add(new Customer(id, name));
            Console.WriteLine("Customer added successfully.");
        }

        private void AddOrder()
        {
            Console.Write("\nEnter order name: ");
            string name = Console.ReadLine();
            Console.Write("Enter customer ID for this order: ");
            int customerId = int.Parse(Console.ReadLine());
            int id = orderManager.Get().Count + 1;
            orderManager.Add(new Order(id, name, customerId));
            Console.WriteLine("Order added successfully.");
        }
    }
}
