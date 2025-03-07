using Microsoft.Data.SqlClient;
using POS.Data;
using POS.model;
using System;
using System.Collections.Generic;
using System.Data;

namespace POS.Services
{
    public class CustomerService
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();

        public void AddCustomer(string name, string email, string phone)
        {
            dbHelper.ExecuteNonQuery("sp_insert_customer", cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@c_name", name);
                cmd.Parameters.AddWithValue("@c_email", email);
                cmd.Parameters.AddWithValue("@c_phone", phone);
            });
            Console.WriteLine("Customer added successfully!");
        }

        public List<Customer> GetCustomers()
        {
            DataTable customersTable = dbHelper.ExecuteQuery("sp_select_customers");
            List<Customer> customers = new List<Customer>();

            foreach (DataRow row in customersTable.Rows)
            {
                customers.Add(new Customer
                {
                    Id = Convert.ToInt32(row["c_id"]),
                    Name = row["c_name"].ToString(),
                    Email = row["c_email"].ToString(),
                    Phone = row["c_phone"].ToString()
                });
            }

            return customers;
        }

        public void AddCustomerAndOrder(string name, string email, string phone, decimal price, decimal amount, string status)
        {
            dbHelper.ExecuteNonQuery("sp_insert_customer_order", cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@c_name", name);
                cmd.Parameters.AddWithValue("@c_email", email);
                cmd.Parameters.AddWithValue("@c_phone", phone);
                cmd.Parameters.AddWithValue("@o_price", price);
                cmd.Parameters.AddWithValue("@o_amount", amount);
                cmd.Parameters.AddWithValue("@o_status", status);
            });
            Console.WriteLine("Customer and Order added successfully!");
        }

        public void DeleteCustomer(int customerId)
        {
            dbHelper.ExecuteNonQuery("sp_delete_customer", cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@c_id", customerId);
            });
            Console.WriteLine($"Customer {customerId} deleted successfully!");
        }

        public void UpdateCustomer(int id, string name, string email, string phone)
        {
            try
            {
                dbHelper.ExecuteNonQuery("sp_update_customer", cmd =>
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@c_id", id);
                    cmd.Parameters.AddWithValue("@c_name", name);
                    cmd.Parameters.AddWithValue("@c_email", email);
                    cmd.Parameters.AddWithValue("@c_phone", phone);
                });
                Console.WriteLine("Customer updated successfully.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                SEQConnection sEQConnection = new SEQConnection();
                sEQConnection.write(LogCategory.Error, ex.Message);
            }
        }

        public bool CheckCustomerExist(int id)
        {
            int check = dbHelper.ExecuteScalar("sp_check_customer_exists", cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@c_id", id);
            });

            return check == 1;
        }

        public List<Customer> GetCustomersByID(int id)
        {
            DataTable customersTable = dbHelper.ExecuteQuery("sp_select_customer_by_id", cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@c_id", id);
            });

            List<Customer> customers = new List<Customer>();

            foreach (DataRow row in customersTable.Rows)
            {
                customers.Add(new Customer
                {
                    Id = Convert.ToInt32(row["c_id"]),
                    Name = row["c_name"].ToString(),
                    Email = row["c_email"].ToString(),
                    Phone = row["c_phone"].ToString()
                });
            }

            return customers;
        }
    }
}
