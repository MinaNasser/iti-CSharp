using POS.Data;
using POS.model;
using System;
using System.Collections.Generic;
using System.Data;

namespace POS.Services
{
    public class OrderService
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        public void AddOrder(int customerId, decimal price, decimal amount, string status)
        {
            dbHelper.ExecuteNonQuery("sp_insert_order", cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@o_customer_id", customerId);
                cmd.Parameters.AddWithValue("@o_price", price);
                cmd.Parameters.AddWithValue("@o_amount", amount);
                cmd.Parameters.AddWithValue("@o_status", status);
            });
            Console.WriteLine("Order added successfully!");
        }
        public List<Order> GetOrders()
        {
            DataTable ordersTable = dbHelper.ExecuteQuery("sp_select_orders");
            List<Order> orders = new List<Order>();

            foreach (DataRow row in ordersTable.Rows)
            {
                orders.Add(new Order
                {
                    Id = Convert.ToInt32(row["o_id"]),
                    CustomerId = Convert.ToInt32(row["o_customer_id"]),
                    Price = Convert.ToDecimal(row["o_price"]),
                    Amount = Convert.ToDecimal(row["o_amount"]),
                    Status = row["o_status"].ToString()
                });
            }

            return orders;
        }

        public List<Order> GetOrdersByCustomerId(int customerId)
        {
            DataTable ordersTable = dbHelper.ExecuteQuery("sp_select_orders_by_customer_id", cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@o_customer_id", customerId);
            });

            List<Order> orders = new List<Order>();

            foreach (DataRow row in ordersTable.Rows)
            {
                orders.Add(new Order
                {
                    Id = Convert.ToInt32(row["o_id"]),
                    CustomerId = Convert.ToInt32(row["o_customer_id"]),
                    Price = Convert.ToDecimal(row["o_price"]),
                    Amount = Convert.ToDecimal(row["o_amount"]),
                    Status = row["o_status"].ToString()
                });
            }

            return orders;
        }
        public void UpdateOrder(int orderId, decimal price, decimal amount, string status)
        {
            try
            {
                dbHelper.ExecuteNonQuery("sp_update_order", cmd =>
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@o_id", orderId);
                    cmd.Parameters.AddWithValue("@o_price", price);
                    cmd.Parameters.AddWithValue("@o_amount", amount);
                    cmd.Parameters.AddWithValue("@o_status", status);
                });
                Console.WriteLine("Order updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating order: {ex.Message}");
            }
        }
        public void DeleteOrder(int orderId)
        {
            dbHelper.ExecuteNonQuery("sp_delete_order", cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@o_id", orderId);
            });
            Console.WriteLine($"Order {orderId} deleted successfully!");
        }

        public bool CheckOrderExist(int orderId)
        {
            int check = dbHelper.ExecuteScalar("sp_check_order_exists", cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@o_id", orderId);
            });

            return check == 1;
        }
    }
}
