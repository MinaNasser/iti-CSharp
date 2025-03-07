using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data
{
    public class DatabaseHelper
    {
        SEQConnection sEQConnection = new SEQConnection();


        string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
        public void ExecuteNonQuery(string procedureName, Action<SqlCommand> paramAction = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        paramAction?.Invoke(cmd);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL :", ex.Message);
                    sEQConnection.write(LogCategory.Warning, ex.StackTrace);
                    sEQConnection.write(LogCategory.Warning, ex.Message);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    sEQConnection.write(LogCategory.Warning, ex.StackTrace);

                    sEQConnection.write(LogCategory.Warning, ex.Message);
                }

            }
        }

        public DataTable ExecuteQuery(string procedureName, Action<SqlCommand> paramAction = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        paramAction?.Invoke(cmd);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL :", ex.Message);
                    sEQConnection.write(LogCategory.Warning, ex.StackTrace);
                    sEQConnection.write(LogCategory.Warning, ex.Message);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    sEQConnection.write(LogCategory.Warning, ex.StackTrace);

                    sEQConnection.write(LogCategory.Warning, ex.Message);
                }
                return null;
            }
        }

        public int ExecuteScalar(string procedureName, Action<SqlCommand> parameterSetter)
        {
            int result = 0;
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    parameterSetter(cmd);

                    try
                    {
                        conn.Open();
                        object scalarValue = cmd.ExecuteScalar();
                        if (scalarValue != null)
                        {
                            result = Convert.ToInt32(scalarValue);
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQL :", ex.Message);
                        sEQConnection.write(LogCategory.Warning, ex.StackTrace);
                        sEQConnection.write(LogCategory.Warning, ex.Message);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        sEQConnection.write(LogCategory.Warning, ex.StackTrace);
                 
                        sEQConnection.write(LogCategory.Warning, ex.Message);
                    }
                    
                }
            }
            return result;
        }

    }
}
