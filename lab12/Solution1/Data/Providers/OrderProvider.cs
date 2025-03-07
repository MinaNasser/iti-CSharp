using Data.Connections;
using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;

namespace Data.Providers
{
    class OrderEnumerator : IEnumerator<Order>
    {
        private int top = -1;
        private List<Order> list;

        public OrderEnumerator(List<Order> _list)
        {
            list = _list ?? throw new ArgumentNullException(nameof(_list));
        }

        public Order Current
        {
            get
            {
                if (top < 0 || top >= list.Count)
                    throw new InvalidOperationException("Enumerator is out of bounds.");
                return list[top];
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            if (top + 1 < list.Count)
            {
                top++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            top = -1;
        }
    }

    public class OrderProvider : IList<Order>
    {
        private SQLConnectionHelper _dbHelper = new SQLConnectionHelper();
        private SEQConnection sEQ = new SEQConnection();
        public OrderProvider(SQLConnectionHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        }
        public Order this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count
        {
            get
            {
                try
                {
                    object result = _dbHelper.ExecuteScalar("sp_count_orders", cmd => { });
                    return result != null ? Convert.ToInt32(result) : 0;
                }
                catch (Exception ex)
                {
                    sEQ.write(LogCategory.Error, ex.Message);
                    return 0;
                }
            }
        }

        public bool IsReadOnly => false;

        public void Add(Order item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Order object cannot be null.");

            try
            {
                _dbHelper.ExecuteNonQuery("sp_insert_order", cmd =>
                {
                    cmd.Parameters.AddWithValue("o_customer_id", item.CustomerID);
                    cmd.Parameters.AddWithValue("@o_price", item.Price);
                    cmd.Parameters.AddWithValue("@o_amount", item.Amount);
                    cmd.Parameters.AddWithValue("@o_status", item.Status.ToString());
                });
            }
            catch (Exception ex)
            {
                sEQ.write(LogCategory.Error, $"Error inserting order: {ex.Message}");
                throw;
            }
        }


        public void Clear()
        {
            try
            {
                _dbHelper.ExecuteNonQuery("sp_delete_all_orders");
            }
            catch (Exception ex)
            {
                sEQ.write(LogCategory.Error, $"Error deleting all orders: {ex.Message}");
                throw;
            }
        }

        public bool Contains(Order item)
        {
            if (item == null) return false;
            return _dbHelper.ExecuteScalar("sp_check_order_exists", cmd =>
            {
                cmd.Parameters.AddWithValue("@o_id", item.Id);
            }) == 1;
        }

        public void CopyTo(Order[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Order> GetEnumerator()
        {
            return new OrderEnumerator(GetAllOrders());
        }

        public int IndexOf(Order item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Order item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int orderID)
        {
            int rowsAffected = _dbHelper.ExecuteNonQuery("sp_delete_order_by_order_id", cmd =>
            {
                cmd.Parameters.AddWithValue("@o_id", orderID);
            });

            return rowsAffected > 0;
        }


        public void RemoveAt(int index)
        {
            try
            {
                List<Order> orders = GetAllOrders();

                if (index < 0 || index >= orders.Count)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");

                Order orderToRemove = orders[index];

                int rowsAffected = _dbHelper.ExecuteNonQuery("sp_delete_order_by_order_id", cmd =>
                {
                    cmd.Parameters.AddWithValue("@o_id", orderToRemove.Id);
                });

                if (rowsAffected == 0)
                    throw new Exception($"Failed to remove order with ID {orderToRemove.Id}.");
            }
            catch (Exception ex)
            {
                sEQ.write(LogCategory.Error, $"Error in RemoveAt: {ex.Message}");
                throw;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private List<Order> MapOrdersFromDataTable(DataTable dt)
        {
            List<Order> orders = new List<Order>();

            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    //Console.WriteLine($"Row Data: {string.Join(", ", row.ItemArray)}"); // Debugging

                    orders.Add(new Order
                    {
                        Id = Convert.ToInt32(row["o_id"]),
                        CustomerID = Convert.ToInt32(row["o_customer_id"]),
                        Price = Convert.ToDecimal(row["o_price"]),
                        Amount = Convert.ToDecimal(row["o_amount"]),
                        Status = Enum.TryParse(row["o_status"].ToString(), out OrderStatus status) ? status : OrderStatus.Pending,

                        UpdatedAt = row["o_updatedAt"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["o_updatedAt"]),
                        CreatedAt = row["o_createdAt"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["o_createdAt"])

                    });
                }
                catch (Exception ex)
                {
                    sEQ.write(LogCategory.Error, $"Error mapping order data: {ex.Message}");
                }
            }

            return orders;
        }

        public List<Order> GetAllOrders()
        {
            DataTable dt = _dbHelper.ExecuteQuery("sp_select_orders");
            return MapOrdersFromDataTable(dt);
        }

        public List<Order> GetOrderByID(int id)
        {
            DataTable dt = _dbHelper.ExecuteQuery("sp_select_order_by_id", cmd =>
            {
                cmd.Parameters.AddWithValue("@o_id", id);
            });
            return MapOrdersFromDataTable(dt);
        }

        public void UpdateOrder(int id, decimal price, decimal amount, OrderStatus status)
        {
            Console.WriteLine($"Updating Order {id}...");
            sEQ.write(LogCategory.Information, $"Updating Order {id}: {price}, {amount}, {status.ToString()}");

            try
            {
                int rowsAffected = _dbHelper.ExecuteNonQuery("sp_update_order", cmd =>
                {
                    cmd.Parameters.AddWithValue("@o_id", id);
                    cmd.Parameters.AddWithValue("@o_price", price);
                    cmd.Parameters.AddWithValue("@o_amount", amount);
                    cmd.Parameters.AddWithValue("@o_status", status.ToString());
                });
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Order {id} updated successfully.");
                    sEQ.write(LogCategory.Information, $"Order {id} updated successfully.");
                }
                else
                {
                    Console.WriteLine($"No Order was updated. Please check the ID.");
                    sEQ.write(LogCategory.Warning, $"Update failed: No Order found with ID {id}.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                sEQ.write(LogCategory.Error, $"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                sEQ.write(LogCategory.Error, $"Unexpected Error: {ex.Message}");
            }
        }

        public bool Remove(Order item)
        {
            if (item == null) return false;

            int rowsAffected = _dbHelper.ExecuteNonQuery("sp_delete_order_by_order_id", cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@o_id", item.Id);
            });

            return rowsAffected > 0;
        }


        

        public bool UpdateOrderByCustomerName(string customerName, int orderId, decimal price, decimal amount, OrderStatus status)
        {
            try
            {
                int rowsAffected = _dbHelper.ExecuteNonQuery("sp_update_order_by_id", cmd =>
                {
                    cmd.CommandType = CommandType.StoredProcedure; 
                    cmd.Parameters.AddWithValue("@o_id", orderId);
                    cmd.Parameters.AddWithValue("@o_price", price);
                    cmd.Parameters.AddWithValue("@o_amount", amount);
                    cmd.Parameters.AddWithValue("@o_status", status.ToString());
                });

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Order {orderId} for customer '{customerName}' updated successfully.");
                    sEQ.write(LogCategory.Information, $"Order {orderId} updated successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"No order updated. Check if order ID is correct.");
                    sEQ.write(LogCategory.Warning, $"No order updated for customer '{customerName}'.");
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                sEQ.write(LogCategory.Error, $"SQL Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                sEQ.write(LogCategory.Error, $"Unexpected Error: {ex.Message}");
                return false;
            }
        }

        public List<Order> GetOrdersByCustomerName(string customerName)
        {
            List<Order> orders = new List<Order>();

            _dbHelper.ExecuteReader("sp_get_orders_by_customer_name", cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure; 
                cmd.Parameters.AddWithValue("@c_name", customerName);
            },
            reader =>
            {
                while (reader.Read())
                {
                    orders.Add(new Order
                    {
                        Id = reader.GetInt32(0),
                        Price = reader.GetDecimal(1),
                        Amount = reader.GetDecimal(2),
                        Status = Enum.TryParse<OrderStatus>(reader.GetString(3), out var status) ? status : OrderStatus.Pending

                    });
                }
            });

            return orders;
        }


        public bool UpdateOrderById(int orderId, decimal price, decimal amount, OrderStatus status)
        {
            try
            {
               
                int rowsAffected = _dbHelper.ExecuteNonQuery("sp_update_order_by_id", cmd =>
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@o_id", orderId);
                    cmd.Parameters.AddWithValue("@o_price", price);
                    cmd.Parameters.AddWithValue("@o_amount", amount);
                    cmd.Parameters.AddWithValue("@o_status", status.ToString());
                });
                return true;

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return false;
            }
        }

        public void AddOrderWithInventory(int customerId, string itemsJson)
        {
            _dbHelper.ExecuteNonQuery("sp_create_order", cmd =>
            {
                cmd.Parameters.AddWithValue("@customer_id", customerId);
                cmd.Parameters.AddWithValue("@items", itemsJson);
            });
        }
        public int AddOrderAndReturnId(Order order)
        {
            object result = _dbHelper.ExecuteScalar("sp_insert_order", cmd =>
            {
                cmd.Parameters.AddWithValue("@o_customer_id", order.CustomerID);
                cmd.Parameters.AddWithValue("@o_price", order.Price);
                cmd.Parameters.AddWithValue("@o_amount", order.Amount);
                cmd.Parameters.AddWithValue("@o_status", order.Status.ToString());
            });

            return result != null ? Convert.ToInt32(result) : 0;
        }





    }
}
