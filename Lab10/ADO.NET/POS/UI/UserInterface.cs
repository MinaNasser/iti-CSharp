using System;
using System.Collections.Generic;
using POS.Services;

using POS.model;

namespace POS
{
    public class UserInterface
    {
        private readonly CustomerService customerService = new CustomerService();
        private readonly OrderService orderService = new OrderService();
        private readonly SEQConnection sEQConnection = new SEQConnection();

        public  void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("\n===== MAIN MENU =====");
                Console.WriteLine("1- Customer Operations");
                Console.WriteLine("2- Order Operations");
                Console.WriteLine("3- Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowCustomerMenu();
                        break;
                    case "2":
                        ShowOrderMenu();
                        break;
                    case "3":
                        Console.WriteLine("Exiting the application...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void ShowCustomerMenu()
        {
            while (true)
            {
                Console.WriteLine("\n===== CUSTOMER MENU =====");
                Console.WriteLine("1- Add Customer");
                Console.WriteLine("2- Show Customers");
                Console.WriteLine("3- Edit Customer");
                Console.WriteLine("4- Delete Customer");
                Console.WriteLine("5- Search Customer by ID");
                Console.WriteLine("6- Add Customer & Order");
                Console.WriteLine("7- Back to Main Menu");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddCustomer();
                        break;
                    case "2":
                        ShowCustomers();
                        break;
                    case "3":
                        EditCustomer();
                        break;
                    case "4":
                        DeleteCustomer();
                        break;
                    case "5":
                        SearchById();
                        break;
                    case "6":
                        AddCustomerWithOrder();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void ShowOrderMenu()
        {
            while (true)
            {
                Console.WriteLine("\n===== ORDER MENU =====");
                Console.WriteLine("1- Add Order");
                Console.WriteLine("2- Show Orders");
                Console.WriteLine("3- Delete Order");
                Console.WriteLine("4- Back to Main Menu");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddOrder();
                        break;
                    case "2":
                        ShowOrders();
                        break;
                    case "3":
                        DeleteOrder();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void SearchById()
        {
            Console.Write("Enter Customer ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid input. Please enter a valid ID.");
                return;
            }

            if (customerService.CheckCustomerExist(id))
            {
                var customer = customerService.GetCustomersByID(id);
                Console.WriteLine($"Customer Found: {customer[0].Name} | {customer[0].Email} | {customer[0].Phone}");
            }
            else
            {
                Console.WriteLine("This Customer Not Found.");
            }
        }

        private void EditCustomer()
        {
            Console.Write("Enter Customer ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid input. Please enter a valid ID.");
                return;
            }

            if (!customerService.CheckCustomerExist(id))
            {
                Console.WriteLine("Customer Not Found.");
                return;
            }

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();

            customerService.UpdateCustomer(id, name, email, phone);
            Console.WriteLine("Customer updated successfully.");
        }

        private void DeleteOrder()
        {
            Console.Write("Enter Order ID: ");
            if (!int.TryParse(Console.ReadLine(), out int orderID))
            {
                Console.WriteLine("Invalid input. Please enter a valid Order ID.");
                return;
            }

            orderService.DeleteOrder(orderID);
            Console.WriteLine("Order deleted successfully.");
        }

        private void DeleteCustomer()
        {
            Console.Write("Enter Customer ID: ");
            if (!int.TryParse(Console.ReadLine(), out int customerID))
            {
                Console.WriteLine("Invalid input. Please enter a valid Customer ID.");
                return;
            }

            customerService.DeleteCustomer(customerID);
            Console.WriteLine("Customer deleted successfully.");
        }

        private void AddCustomer()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();

            try
            {
                customerService.AddCustomer(name, email, phone);
                Console.WriteLine("Customer added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                sEQConnection.write(LogCategory.Error, ex.Message);
            }
        }

        private void ShowCustomers()
        {
            var customers = customerService.GetCustomers();

            if (customers.Count == 0)
            {
                Console.WriteLine("No customers found.");
                return;
            }

            Console.WriteLine("Customers List:");
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Id} | {customer.Name} | {customer.Email} | {customer.Phone}");
            }
        }

        private void AddOrder()
        {
            Console.Write("Enter Customer ID: ");
            if (!int.TryParse(Console.ReadLine(), out int custId))
            {
                Console.WriteLine("Invalid input for Customer ID.");
                return;
            }

            Console.Write("Enter Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid input for Price.");
                return;
            }

            Console.Write("Enter Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid input for Amount.");
                return;
            }

            Console.Write("Enter Status (1 - Pending, 2 - Completed, 3 - Cancelled): ");
            string input = Console.ReadLine();

            orderStatus status = input switch
            {
                "1" => orderStatus.pending,
                "2" => orderStatus.completed,
                "3" => orderStatus.cancelled,
                _ => orderStatus.pending
            };

            orderService.AddOrder(custId, price, amount, status.ToString());
            Console.WriteLine("Order added successfully!");
        }

        private void ShowOrders()
        {
            var orders = orderService.GetOrders();

            if (orders.Count == 0)
            {
                Console.WriteLine("No orders found.");
                return;
            }

            Console.WriteLine("Orders List:");
            foreach (var order in orders)
            {
                Console.WriteLine($"{order.Id} | Customer ID: {order.CustomerId} | Price: {order.Price} | Amount: {order.Amount} | Status: {order.Status}");
            }
        }

// public void AddCustomerAndOrder(string name, string email, string phone, decimal price, decimal amount, string status)
//         {
//             _dbHelper.ExecuteNonQuery("sp_insert_customer_order", cmd =>
//             {
//                 cmd.Parameters.AddWithValue("@c_name", name);
//                 cmd.Parameters.AddWithValue("@c_email", email);
//                 cmd.Parameters.AddWithValue("@c_phone", phone);
//                 cmd.Parameters.AddWithValue("@o_price", price);
//                 cmd.Parameters.AddWithValue("@o_amount", amount);
//                 cmd.Parameters.AddWithValue("@o_status", status);
//             });
//         }

    
}
