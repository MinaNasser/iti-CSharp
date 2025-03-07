using Data.Connections;
using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Data.Providers
{
    public class CustomerProvider : IList<Customer>
    {
        private readonly SQLConnectionHelper _dbHelper;

        public CustomerProvider(SQLConnectionHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        }

        public Customer this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count
        {
            get
            {
                object result = _dbHelper.ExecuteScalar("sp_count_customers", cmd => { });
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }
        public bool IsReadOnly => false;

        public void Add(Customer item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Customer object cannot be null.");

            _dbHelper.ExecuteNonQuery("sp_insert_customer", cmd =>
            {
                cmd.Parameters.AddWithValue("@c_name", item.Name);
                cmd.Parameters.AddWithValue("@c_email", item.Email);
                cmd.Parameters.AddWithValue("@c_phone", item.Phone);
            });
        }

        public void Clear() => _dbHelper.ExecuteNonQuery("sp_delete_all_customers");

        public bool Contains(Customer item)
        {
            if (item == null) return false;
            return CheckCustomerExist(item.Id);
        }

        public void CopyTo(Customer[] array, int arrayIndex) => throw new NotImplementedException();

        public IEnumerator<Customer> GetEnumerator() => GetAllCustomers().GetEnumerator();

        public int IndexOf(Customer item) => throw new NotImplementedException();

        public void Insert(int index, Customer item) => throw new NotImplementedException();

        public bool Remove(Customer item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Customer object cannot be null.");

            return _dbHelper.ExecuteNonQuery("sp_delete_customer", cmd =>
            {
                cmd.Parameters.AddWithValue("@c_id", item.Id);
            }) > 0;
        }

        public void RemoveAt(int index)
        {
            List<Customer> customers = GetAllCustomers();
            if (index < 0 || index >= customers.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");

            Customer customerToRemove = customers[index];

            _dbHelper.ExecuteNonQuery("sp_delete_customer", cmd =>
            {
                cmd.Parameters.AddWithValue("@c_id", customerToRemove.Id);
            });
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private List<Customer> MapCustomersFromDataTable(DataTable dt)
        {
            List<Customer> customers = new List<Customer>();

            foreach (DataRow row in dt.Rows)
            {
                customers.Add(new Customer
                {
                    Id = Convert.ToInt32(row["c_id"]),
                    Name = row["c_name"].ToString(),
                    Email = row["c_email"].ToString(),
                    Phone = row["c_phone"].ToString(),
                    UpdatedAt = row["c_updatedAt"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["c_updatedAt"]),
                    CreatedAt = row["c_createdAt"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["c_createdAt"])
                });
            }

            return customers;
        }

        public List<Customer> GetAllCustomers()
        {
            DataTable dt = _dbHelper.ExecuteQuery("sp_select_customers");
            return MapCustomersFromDataTable(dt);
        }

        public List<Customer> GetCustomersByID(int id)
        {
            DataTable dt = _dbHelper.ExecuteQuery("sp_select_customer_by_id", cmd =>
            {
                cmd.Parameters.AddWithValue("@c_id", id);
            });
            return MapCustomersFromDataTable(dt);
        }

        public void AddCustomerAndOrder(string name, string email, string phone, decimal price, decimal amount, string status)
        {
            _dbHelper.ExecuteNonQuery("sp_insert_customer_order", cmd =>
            {
                cmd.Parameters.AddWithValue("@c_name", name);
                cmd.Parameters.AddWithValue("@c_email", email);
                cmd.Parameters.AddWithValue("@c_phone", phone);
                cmd.Parameters.AddWithValue("@o_price", price);
                cmd.Parameters.AddWithValue("@o_amount", amount);
                cmd.Parameters.AddWithValue("@o_status", status);
            });
        }

        public void UpdateCustomer(int id, string name, string email, string phone)
        {
            if (!CheckCustomerExist(id))
            {
                throw new InvalidOperationException($"No customer found with ID {id}.");
            }

            _dbHelper.ExecuteNonQuery("sp_update_customer", cmd =>
            {
                cmd.Parameters.AddWithValue("@c_id", id);
                cmd.Parameters.AddWithValue("@c_name", name);
                cmd.Parameters.AddWithValue("@c_email", email);
                cmd.Parameters.AddWithValue("@c_phone", phone);
            });
        }

        public bool CheckCustomerExist(int id)
        {
            object result = _dbHelper.ExecuteScalar("sp_check_customer_exists", cmd =>
            {
                cmd.Parameters.AddWithValue("@c_id", id);
            });

            return Convert.ToInt32(result ?? 0) > 0;
        }

        public bool CheckCustomerExist(string name)
        {
            object result = _dbHelper.ExecuteScalar("sp_check_customer_exists_by_name", cmd =>
            {
                cmd.Parameters.AddWithValue("@c_name", name.Trim());
            });

            return Convert.ToInt32(result ?? 0) > 0;
        }
    }
}
