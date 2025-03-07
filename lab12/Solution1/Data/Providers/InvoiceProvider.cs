using Data.Connections;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace Data.Providers
{
    public class InvoiceProvider
    {
        private readonly SQLConnectionHelper _dbHelper;

        public InvoiceProvider(SQLConnectionHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        }

        public int SaveInvoice(Invoice invoice)
        {
            Console.WriteLine($"DEBUG: Checking Order ID {invoice.OrderId} before inserting invoice...");

            object orderExists = _dbHelper.ExecuteScalar("SELECT COUNT(1) FROM Orders WHERE o_id = @order_id", cmd =>
            {
                cmd.Parameters.AddWithValue("@order_id", invoice.OrderId);
            });

            if (orderExists == null || Convert.ToInt32(orderExists) == 0)
            {
                Console.WriteLine($"❌ Error: Order ID {invoice.OrderId} not found in database. Retrying in 100ms...");
                Thread.Sleep(100); 
                return 0;
            }

            object insertedId = _dbHelper.ExecuteScalar(@"
                INSERT INTO Invoices (invoice_number, order_id, customer_id, total_amount, status)
                VALUES (@invoice_number, @order_id, @customer_id, @total_amount, @status);
                SELECT SCOPE_IDENTITY();", cmd =>
            {
                cmd.Parameters.AddWithValue("@invoice_number", invoice.InvoiceNumber);
                cmd.Parameters.AddWithValue("@order_id", invoice.OrderId);
                cmd.Parameters.AddWithValue("@customer_id", invoice.CustomerId);
                cmd.Parameters.AddWithValue("@total_amount", invoice.TotalAmount);
                cmd.Parameters.AddWithValue("@status", invoice.Status);
            });

            if (insertedId != null && int.TryParse(insertedId.ToString(), out int invoiceId))
            {
                Console.WriteLine($"✅ Invoice saved successfully! Invoice ID: {invoiceId}");
                return invoiceId;
            }
            else
            {
                Console.WriteLine("❌ Failed to save invoice.");
                return 0;
            }
        }


        public void UpdateInventoryQuantity(int itemId, int newQuantity)
        {
            _dbHelper.ExecuteNonQuery("sp_update_inventory_quantity", cmd =>
            {
                cmd.Parameters.AddWithValue("@item_id", itemId);
                cmd.Parameters.AddWithValue("@quantity", newQuantity);
            });
            Console.WriteLine($"✅ Inventory updated: Item ID {itemId}, New Quantity: {newQuantity}");
        }

        public List<Invoice> GetAllInvoices()
        {
            DataTable dt = _dbHelper.ExecuteQuery("sp_get_invoices");

            if (dt == null || dt.Rows.Count == 0)
            {
                Console.WriteLine("⚠️ No invoices found.");
                return new List<Invoice>();
            }

            List<Invoice> invoices = new List<Invoice>();

            foreach (DataRow row in dt.Rows)
            {
                invoices.Add(new Invoice
                {
                    Id = Convert.ToInt32(row["invoice_id"]),
                    InvoiceNumber = row["invoice_number"].ToString(),
                    OrderId = Convert.ToInt32(row["order_id"]),
                    CustomerId = Convert.ToInt32(row["customer_id"]),
                    TotalAmount = Convert.ToDecimal(row["total_amount"]),
                    Status = row["status"].ToString(),
                    CreatedAt = Convert.ToDateTime(row["created_at"])
                });
            }

            return invoices;
        }


        public Invoice GetInvoiceByOrderId(int orderId)
        {
            DataTable dt = _dbHelper.ExecuteQuery("sp_get_invoice_by_order_id", cmd =>
            {
                cmd.Parameters.AddWithValue("@order_id", orderId);
            });

            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];

            return new Invoice
            {
                Id = Convert.ToInt32(row["invoice_id"]),
                InvoiceNumber = row["invoice_number"].ToString(),
                OrderId = Convert.ToInt32(row["order_id"]),
                CustomerId = Convert.ToInt32(row["customer_id"]),
                TotalAmount = Convert.ToDecimal(row["total_amount"]),
                Status = row["status"].ToString(),
                CreatedAt = Convert.ToDateTime(row["created_at"])
            };
        }

        public List<Invoice> GetInvoicesByCustomerName(string customerName)
        {
            Console.WriteLine($"?? Fetching invoices for customer: {customerName}");

            DataTable dt = _dbHelper.ExecuteQuery("sp_get_invoices_by_customer_name", cmd =>
            {
                cmd.Parameters.AddWithValue("@customer_name", customerName);
            });

            Console.WriteLine($"?? Rows returned: {dt.Rows.Count}");

            List<Invoice> invoices = new List<Invoice>();

            foreach (DataRow row in dt.Rows)
            {
                invoices.Add(new Invoice
                {
                    Id = Convert.ToInt32(row["invoice_id"]),
                    OrderId = Convert.ToInt32(row["order_id"]),
                    CustomerId = Convert.ToInt32(row["customer_id"]),
                    TotalAmount = Convert.ToDecimal(row["total_amount"]),
                    CreatedAt = Convert.ToDateTime(row["created_at"]),
                    Status = row["order_status"].ToString()
                });
            }

            Console.WriteLine($"? {invoices.Count} invoices found.");

            return invoices;
        }


    }
}
