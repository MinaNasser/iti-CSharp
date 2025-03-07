using Data.Connections;
using Data.Providers;
using Manegers;
using Models;
using System;
using System.Collections.Generic;
using UI;


namespace POS
{
    public class UserInterface
    {
        private readonly CustomerProvider customerProvider;
        private readonly OrderProvider orderProvider;
        private readonly InventoryProvider inventoryProvider;
        private readonly OrderManger orderManger;
        private readonly SEQConnection sEQConnection;
        private readonly InvoiceProvider invoiceProvider;


        public UserInterface()
        {
            SQLConnectionHelper dbHelper = new SQLConnectionHelper();
            customerProvider = new CustomerProvider(dbHelper);
            orderProvider = new OrderProvider(dbHelper);
            inventoryProvider = new InventoryProvider(dbHelper);
            orderManger = new OrderManger(orderProvider);
            sEQConnection = new SEQConnection();
            invoiceProvider = new InvoiceProvider(dbHelper);
        }


        public void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("\n===== MAIN MENU =====");
                Console.WriteLine("1- Customer Operations");
                Console.WriteLine("2- Order Operations");
                Console.WriteLine("3- Inventory Operations");
                Console.WriteLine("4- Invoice Operations"); // ✅ خيار جديد للفواتير
                Console.WriteLine("5- Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": ShowCustomerMenu(); break;
                    case "2": ShowOrderMenu(); break;
                    case "3": ShowInventoryMenu(); break;
                    case "4": ShowInvoiceMenu(); break; // ✅ استدعاء القائمة الجديدة
                    case "5": Console.WriteLine("Exiting the application..."); return;
                    default: Console.WriteLine("Invalid choice. Please try again."); break;
                }
            }
        }
        #region Customer

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
                "3" => OrderStatus.Cancelled,
                _ => OrderStatus.Pending
            };
            customerProvider.AddCustomerAndOrder(name, email, phone, price, amount, status.ToString());
            Console.WriteLine("Customer and order added successfully!");
        }

        #endregion
        #region Order
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

        private void AddOrder()
        {
            try
            {
                Console.Write("Enter Customer ID: ");
                if (!int.TryParse(Console.ReadLine(), out int customerId))
                {
                    Console.WriteLine("❌ Invalid Customer ID.");
                    return;
                }

                // ✅ التأكد من وجود العميل
                if (!customerProvider.CheckCustomerExist(customerId))
                {
                    Console.WriteLine("⚠️ Customer ID not found.");
                    return;
                }

                // ✅ جلب المخزون
                List<ItemStock> stock = inventoryProvider.GetStock();
                if (stock.Count == 0)
                {
                    Console.WriteLine("⚠️ No items available in inventory!");
                    return;
                }

                List<OrderItem> orderItems = new List<OrderItem>();

                while (true)
                {
                    Console.WriteLine("\n📦 Select an item from inventory:");
                    for (int i = 0; i < stock.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {stock[i].Item.Name} | Available: {stock[i].Quantity}");
                    }

                    Console.Write("🔹 Enter item number (or 0 to finish): ");
                    if (!int.TryParse(Console.ReadLine(), out int itemIndex) || itemIndex < 1 || itemIndex > stock.Count)
                    {
                        Console.WriteLine("❌ Invalid item selection.");
                        continue;
                    }

                    itemIndex -= 1;
                    ItemStock selectedStock = stock[itemIndex];

                    Console.Write($"🔹 Enter quantity (Available: {selectedStock.Quantity}): ");
                    if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0 || qty > selectedStock.Quantity)
                    {
                        Console.WriteLine("❌ Invalid quantity.");
                        continue;
                    }

                    Console.Write("💰 Enter price per unit: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
                    {
                        Console.WriteLine("❌ Invalid price.");
                        continue;
                    }

                    orderItems.Add(new OrderItem
                    {
                        Item = selectedStock.Item,
                        Qty = qty,
                        Price = price
                    });

                    Console.Write("➕ Do you want to add another item? (y/n): ");
                    if (Console.ReadLine().ToLower() != "y") break;
                }

                if (orderItems.Count == 0)
                {
                    Console.WriteLine("❌ Order must contain at least one item.");
                    return;
                }

                // ✅ تأكيد قبل الحفظ
                Console.WriteLine("\n🛒 Order Summary:");
                foreach (var item in orderItems)
                {
                    Console.WriteLine($"- {item.Item.Name} | Qty: {item.Qty} | Price: {item.Price:C}");
                }
                Console.Write("📝 Confirm order? (y/n): ");
                if (Console.ReadLine().ToLower() != "y")
                {
                    Console.WriteLine("❌ Order cancelled.");
                    return;
                }

                // ✅ إنشاء الطلب
                Order newOrder = new Order
                {
                    CustomerID = customerId,
                    Price = orderItems.Sum(i => i.Qty * i.Price),
                    Amount = orderItems.Sum(i => i.Qty),
                    Status = OrderStatus.Pending
                };

                int orderId = orderManger.AddAndReturnId(newOrder);
                if (orderId == 0)
                {
                    Console.WriteLine("❌ Failed to create order.");
                    return;
                }

                Console.WriteLine($"✅ Order created successfully! Order ID: {orderId}");

                // ✅ تحديث المخزون
                foreach (var orderItem in orderItems)
                {
                    bool success = inventoryProvider.DecreaseItems(orderItem.Item.Id, orderItem.Qty);
                    if (!success)
                    {
                        Console.WriteLine($"⚠️ Failed to update inventory for item {orderItem.Item.Id}");
                        // ✅ استرجاع المخزون عند الفشل
                        foreach (var rollbackItem in orderItems)
                        {
                            inventoryProvider.AddItemToInventory(rollbackItem.Item.Id, rollbackItem.Qty);
                        }
                        Console.WriteLine("🔄 Inventory rollback completed.");
                        return;
                    }
                }

                // ✅ إنشاء الفاتورة
                Invoice newInvoice = new Invoice
                {
                    InvoiceNumber = "INV-" + DateTime.UtcNow.ToString("yyyyMMddHHmmss"),
                    OrderId = orderId,
                    CustomerId = customerId,
                    TotalAmount = newOrder.Price,
                    Status = "pending",
                    CreatedAt = DateTime.UtcNow
                };

                int invoiceId = invoiceProvider.SaveInvoice(newInvoice);
                if (invoiceId > 0)
                {
                    Console.WriteLine($"📜 Invoice saved successfully! Invoice ID: {invoiceId}");
                }
                else
                {
                    Console.WriteLine("❌ Failed to save invoice.");
                }

                
                Console.Write("🖨️ Do you want to print the invoice? (y/n): ");
                if (Console.ReadLine().ToLower() == "y")
                {
                    new InvoicePrinter().Print(newInvoice);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                sEQConnection.write(LogCategory.Error, ex.ToString());
            }
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

        #endregion
        #region Inventory

        private void ShowInventoryMenu()
        {
            while (true)
            {
                Console.WriteLine("\n===== INVENTORY MENU =====");
                Console.WriteLine("1- Add Item to Inventory");
                Console.WriteLine("2- View Inventory");
                Console.WriteLine("3- Update Inventory Quantity"); // ✅ خيار جديد
                Console.WriteLine("4- Back to Main Menu");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": AddItemToInventory(); break;
                    case "2": ViewInventory(); break;
                    case "3": UpdateInventoryQuantity(); break; // ✅ استدعاء الدالة الجديدة
                    case "4": return;
                    default: Console.WriteLine("Invalid choice. Please try again."); break;
                }
            }
        }

        private void AddItemToInventory()
        {
            Console.Clear();
            Console.Write("Enter Item Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            if (quantity <= 0)
            {
                Console.WriteLine(" Invalid quantity.");
                return;
            }

            int itemId = inventoryProvider.AddNewItem(name, description);

            if (itemId > 0)
            {
                inventoryProvider.AddItemToInventory(itemId, quantity);
                Console.WriteLine($" Item '{name}' (ID: {itemId}) added to inventory with {quantity} units.");
            }
            else
            {
                Console.WriteLine("❌ Failed to add item.");
            }
        }


        private void ViewInventory()
        {
            Console.Clear();
            List<ItemStock> stock = inventoryProvider.GetStock();

            if (stock.Count == 0)
            {
                Console.WriteLine("⚠️ No items in inventory!");
                return;
            }

            Console.WriteLine("\n===== INVENTORY STOCK =====");
            Console.WriteLine($"{"ID",-5} {"Name",-20} {"Description",-30} {"Quantity",-10}");
            Console.WriteLine(new string('-', 70));

            foreach (var itemStock in stock)
            {
                Console.WriteLine($"{itemStock.Item.Id,-5} {itemStock.Item.Name,-20} {itemStock.Item.Description,-30} {itemStock.Quantity,-10}");
            }
        }

        private void UpdateInventoryQuantity()
        {
            Console.Clear();
            Console.Write("Enter Item ID to update quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int itemId))
            {
                Console.WriteLine("❌ Invalid Item ID.");
                return;
            }

            Console.Write("Enter New Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int newQuantity) || newQuantity < 0)
            {
                Console.WriteLine("❌ Invalid quantity.");
                return;
            }

            inventoryProvider.UpdateInventoryQuantity(itemId, newQuantity);
            Console.WriteLine("✅ Inventory quantity updated successfully!");
        }

        #endregion
        #region Invoice
        private void ShowInvoiceMenu()
        {
            while (true)
            {
                Console.WriteLine("\n===== INVOICE MENU =====");
                Console.WriteLine("1- Show All Invoices");
                Console.WriteLine("2- Print Invoice for an Order");
                Console.WriteLine("3- View Invoices by Customer Name");
                Console.WriteLine("4- View Invoice by Order ID");
                Console.WriteLine("5- Back to Main Menu");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": GetAllInvoices();break;
                    case "2": PrintInvoice(); break;
                    case "3": ViewInvoicesByCustomerName(); break;
                    case "4": ViewInvoiceByOrderId(); break;
                    case "5": return;
                    default: Console.WriteLine("Invalid choice. Please try again."); break;
                }
            }
        }
        private void PrintInvoice()

        {
            Console.Write("Enter Order ID to print invoice: ");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine(" Invalid Order ID.");
                return;
            }

            Invoice invoice = invoiceProvider.GetInvoiceByOrderId(orderId);
            invoiceProvider.SaveInvoice(invoice);
            Console.WriteLine("Invoice Saved");
            if (invoice == null)
            {
                Console.WriteLine(" No invoice found for this order.");
                return;
            }

            Console.WriteLine(" Printing Invoice...");

            new InvoicePrinter().Print(invoice);
            Console.WriteLine(" Invoice printed successfully!");
        }


        private void ViewInvoicesByCustomerName()
        {
            Console.Write("Enter Customer Name: ");
            string customerName = Console.ReadLine().Trim();

            List<Invoice> invoices = invoiceProvider.GetInvoicesByCustomerName(customerName);

            if (invoices == null || invoices.Count == 0)
            {
                Console.WriteLine("?? No invoices found for this customer.");
                return;
            }

            Console.WriteLine("\n===== INVOICES =====");
            foreach (var invoice in invoices)
            {
                Console.WriteLine($"""
            --------------------------------------
            Invoice ID: {invoice.Id}
            Order ID: {invoice.OrderId}
            Customer ID: {invoice.CustomerId}
            Total Amount: {invoice.TotalAmount:C}
            Created At: {invoice.CreatedAt}
            Status: {invoice.Status}
            --------------------------------------
        """);
            }
        }


        private void ViewInvoiceByOrderId()
        {
            Console.Write("Enter Order ID: ");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("❌ Invalid Order ID.");
                return;
            }

            Invoice invoice = invoiceProvider.GetInvoiceByOrderId(orderId);

            if (invoice == null)
            {
                Console.WriteLine("⚠️ No invoice found for this order.");
                return;
            }

            Console.WriteLine($"\n📜 Invoice ID: {invoice.Id}, Order ID: {invoice.OrderId}, Total: {invoice.TotalAmount:C}, Status: {invoice.Status}");
        }

        private void GetAllInvoices()
        {
            List<Invoice> invoices = invoiceProvider.GetAllInvoices();

            if (invoices.Count == 0)
            {
                Console.WriteLine(" No invoices found.");
                return;
            }

            Console.WriteLine("\n===== 📜 ALL INVOICES 📜 =====");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine($"{"Invoice ID",-10} {"Order ID",-10} {"Customer ID",-12} {"Total Amount",-15} {"Status",-10} {"Created At",-20}");
            Console.WriteLine("----------------------------------------------------------");

            foreach (var invoice in invoices)
            {
                Console.WriteLine($"{invoice.Id,-10} {invoice.OrderId,-10} {invoice.CustomerId,-12} {invoice.TotalAmount,-15:C} {invoice.Status,-10} {invoice.CreatedAt,-20}");
            }

            Console.WriteLine("----------------------------------------------------------");
        }

        #endregion


    }

}

