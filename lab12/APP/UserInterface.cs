using System;
using System.Collections.Generic;
using Data.Providers;
using Models;

namespace POS
{
    public class UserInterface
    {
        private readonly CustomerProvider customerProvider = new CustomerProvider();
        private readonly OrderProvider orderProvider = new OrderProvider();
        private readonly SEQConnection sEQConnection = new SEQConnection();

        public void ShowMainMenu()
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
                    case "1": ShowCustomerMenu(); break;
                    case "2": ShowOrderMenu(); break;
                    case "3": Console.WriteLine("Exiting the application..."); return;
                    default: Console.WriteLine("Invalid choice. Please try again."); break;
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
                Console.WriteLine("6- Add Customer With Order");

                Console.WriteLine("7- Back to Main Menu");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": AddCustomer(); break;
                    case "2": ShowCustomers(); break;
                    case "3": EditCustomer(); break;
                    case "4": DeleteCustomer(); break;
                    case "5": SearchCustomerById(); break;
                    case "6": AddCustomerWithOrder(); break;
                    case "7": return;
                    default: Console.WriteLine("Invalid choice. Please try again."); break;
                }
            }
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
                customerProvider.Add(new Customer()
                {
                    Name = name,
                    Email = email,
                    Phone = phone,
                    UpdatedAt = DateTime.Now,
                });
                Console.WriteLine("Customer added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                sEQConnection.write(LogCategory.Error, ex.Message);
            }
        }
        private void ShowOrderMenu()
        {
            while (true)
            {
                Console.WriteLine("\n===== ORDER MENU =====");
                Console.WriteLine("1- Add Order");
                Console.WriteLine("2- Show Orders");
                Console.WriteLine("3- Edit Order");
                Console.WriteLine("4- Delete Order");
                Console.WriteLine("5- Update Order By Customer name");
                Console.WriteLine("6- Back to Main Menu");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": AddOrder(); break;
                    case "2": ShowOrders(); break;
                    case "3": EditOrder(); break;
                    case "4": DeleteOrder(); break;
                    case "5": UpdateOrderByCustomerName(); break;
 
                    case "6": return;
                    default: Console.WriteLine("Invalid choice. Please try again."); break;
                }
            }
        }

        private void ShowCustomers()
        {
            var customers = customerProvider.GetAllCustomers();

            Console.WriteLine($"Total Customers: {customers.Count}");

            foreach (var customer in customers)
            {
                Console.WriteLine($"""
                        ##################################
                        ID: {customer.Id}
                        Name: {customer.Name}
                        Email: {customer.Email}
                        Phone: {customer.Phone} 
                        Created At :{customer.CreatedAt}
                        Updated At :{customer.UpdatedAt}
                        ###################################

                    """);
            }
        }

        private void ShowOrders()
        {
            var orders = orderProvider.GetAllOrders();
            Console.WriteLine($"Total Orders: {orders.Count}");
            foreach (var order in orders)
            {
                Console.WriteLine($"""
                        ##################################
                        ID: {order.Id}
                        CustomerID: {order.CustomerID}
                        Price: {order.Price}
                        Amount: {order.Amount} 
                        Status: {order.Status}
                        Created At :{order.CreatedAt}
                        Updated At :{order.UpdatedAt}
                        ###################################

                    """);

            }
        }

        private void SearchCustomerById()
        {
            Console.Write("Enter Customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int id) && customerProvider.CheckCustomerExist(id))
            {
                var customer = customerProvider.GetCustomersByID(id);
                Console.WriteLine($"Customer Found: {customer[0].Name} | {customer[0].Email} | {customer[0].Phone}");
            }
            else
            {
                Console.WriteLine("Customer Not Found.");
            }
        }

        private void EditCustomer()
        {
            Console.Write("Enter Customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int id) && customerProvider.CheckCustomerExist(id))
            {
                Console.Write("Enter Name: "); string name = Console.ReadLine();
                Console.Write("Enter Email: "); string email = Console.ReadLine();
                Console.Write("Enter Phone: "); string phone = Console.ReadLine();

                customerProvider.UpdateCustomer(id, name, email, phone);
                Console.WriteLine("Customer updated successfully.");
            }
            else
            {
                Console.WriteLine("Customer Not Found.");
            }
        }

        private void EditOrder()
        {
            Console.Write("Enter Order ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.Write("Enter New Price: "); decimal price = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Enter New Amount: "); decimal amount = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Enter Status (1 - Pending, 2 - Completed, 3 - Cancelled): ");
                string input = Console.ReadLine();

                OrderStatus status = input switch
                {
                    "1" => OrderStatus.Pending,
                    "2" => OrderStatus.Completed,
                    "3" => OrderStatus.Cancelled,
                    _ => OrderStatus.Pending
                };
                orderProvider.UpdateOrder(id, price, amount, status);
                Console.WriteLine("Order updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Order ID.");
            }
        }

        private void DeleteOrder()
        {
            Console.Write("Enter Order ID: ");
            if (int.TryParse(Console.ReadLine(), out int orderID))
            {
                orderProvider.Remove(orderID);
                Console.WriteLine("Order deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Order ID.");
            }
        }

        private void DeleteCustomer()
        {
            Console.Write("Enter Customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int customerID))
            {
                customerProvider.RemoveAt(customerID);
                Console.WriteLine("Customer deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Customer ID.");
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

            OrderStatus status = input switch
            {
                "1" => OrderStatus.Pending,
                "2" => OrderStatus.Completed,
                "3" => OrderStatus.Cancelled,
                _ => OrderStatus.Pending
            };

            orderProvider.Add(new Order()
            {
                CustomerID = custId,
                Price = price,
                Amount = amount,
                Status = status,
                UpdatedAt = DateTime.UtcNow

            });
            Console.WriteLine("Order added successfully!");
        }

        private void AddCustomerWithOrder()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();
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

            OrderStatus status = input switch
            {
                "1" => OrderStatus.Pending,
                "2" => OrderStatus.Completed,
                "3" =>OrderStatus.Cancelled,
                _ => OrderStatus.Pending
            };
            customerProvider.AddCustomerAndOrder(name, email, phone, price, amount, status.ToString()); 
            Console.WriteLine("Customer and order added successfully!");
        }
        private void UpdateOrderByCustomerName()
        {
            try
            {
                Console.Write("Enter Customer Name: ");
                string customerName = Console.ReadLine().Trim().ToLower();

                if (!customerProvider.CheckCustomerExist(customerName))
                {
                    Console.WriteLine("This Customer Not Found.");
                    return;
                }

                List<Order> orders = orderProvider.GetOrdersByCustomerName(customerName);

                if (orders.Count == 0)
                {
                    Console.WriteLine($"No orders found for customer '{customerName}'.");
                    return;
                }

                Console.WriteLine("\nOrders for customer:");
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order ID: {order.Id}, Price: {order.Price}, Amount: {order.Amount}, Status: {order.Status}");
                }

                Console.Write("\nEnter Order ID to update: ");
                if (!int.TryParse(Console.ReadLine(), out int orderId))
                {
                    Console.WriteLine("Invalid Order ID. Please enter a valid number.");
                    return;
                }

                if (!orders.Any(o => o.Id == orderId))
                {
                    Console.WriteLine("Order ID not found for this customer.");
                    return;
                }

                Console.Write("Enter Order Price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.WriteLine("Invalid price format. Please enter a valid number.");
                    return;
                }

                Console.Write("Enter Order Amount: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    Console.WriteLine("Invalid amount format. Please enter a valid number.");
                    return;
                }

                Console.Write("Enter Status (1 - Pending, 2 - Completed, 3 - Cancelled): ");
                string input = Console.ReadLine();

                OrderStatus status = input switch
                {
                    "1" => OrderStatus.Pending,
                    "2" => OrderStatus.Completed,
                    "3" => OrderStatus.Cancelled,
                    _ => OrderStatus.Pending
                };
                Console.WriteLine($"Selected Status: {status}"); 

                bool updated = orderProvider.UpdateOrderById(orderId, price, amount, status);

                Console.WriteLine(updated ? "Update successful" : "Update failed");
                if (updated)
                {
                    Console.WriteLine("Order updated successfully.");
                    sEQConnection.write(LogCategory.Information, "In UI UpdateOrderByCustomerName - Order updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update order. Please check the order ID.");
                    sEQConnection.write(LogCategory.Error, "In UI UpdateOrderByCustomerName - Failed to update order. Please check the order ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                sEQConnection.write(LogCategory.Error, ex.ToString());
            }
        }

        //private void UpdateOrderById()
        //{
        //    try
        //    {
        //        Console.Write("Enter Customer Name: ");
        //        string customerName = Console.ReadLine().Trim();

        //        if (!customerProvider.CheckCustomerExist(customerName))
        //        {
        //            Console.WriteLine("This Customer Not Found.");
        //            return;
        //        }

        //        List<Order> orders = orderProvider.GetOrdersByCustomerName(customerName);

        //        if (orders.Count == 0)
        //        {
        //            Console.WriteLine($"No orders found for customer '{customerName}'.");
        //            return;
        //        }

        //        Console.WriteLine("\nOrders for customer:");
        //        foreach (var order in orders)
        //        {
        //            Console.WriteLine($"Order ID: {order.Id}, Price: {order.Price}, Amount: {order.Amount}, Status: {order.Status}");
        //        }

        //        Console.Write("\nEnter Order ID to update: ");
        //        if (!int.TryParse(Console.ReadLine(), out int orderId))
        //        {
        //            Console.WriteLine("Invalid Order ID. Please enter a valid number.");
        //            return;
        //        }

        //        if (!orders.Any(o => o.Id == orderId))
        //        {
        //            Console.WriteLine("Order ID not found for this customer.");
        //            return;
        //        }

        //        Console.Write("Enter Order Price: ");
        //        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        //        {
        //            Console.WriteLine("Invalid price format. Please enter a valid number.");
        //            return;
        //        }

        //        Console.Write("Enter Order Amount: ");
        //        if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
        //        {
        //            Console.WriteLine("Invalid amount format. Please enter a valid number.");
        //            return;
        //        }

                
        //        Console.Write("Enter Status (1 - Pending, 2 - Completed, 3 - Cancelled): ");
        //        string input = Console.ReadLine();

        //        OrderStatus status = input switch
        //        {
        //            "1" => OrderStatus.Pending,
        //            "2" => OrderStatus.Completed,
        //            "3" => OrderStatus.Cancelled,
        //            _ => OrderStatus.Pending
        //        };

        //        Console.WriteLine($"Selected Status: {status}"); 

        //        bool updated = orderProvider.UpdateOrderById(orderId, price, amount, status);

        //        Console.WriteLine(updated ? "Update successful" : "Update failed");

        //        if (updated)
        //        {
        //            Console.WriteLine("Order updated successfully.");
        //            sEQConnection.write(LogCategory.Information, "In UI UpdateOrderById - Order updated successfully.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Failed to update order. Please check the order ID.");
        //            sEQConnection.write(LogCategory.Error, "In UI UpdateOrderById - Failed to update order. Please check the order ID.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //        sEQConnection.write(LogCategory.Error, ex.ToString());
        //    }
        //}

    }

}

