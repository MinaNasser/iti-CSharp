using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Connections
{
    public class SQLConnectionHelper
    {
        SEQConnection sEQConnection = new SEQConnection();


        //string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnection"].ConnectionString;
        string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionLocal"].ConnectionString;

        internal static SqlConnection GetConnection()
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string procedureName, Action<SqlCommand> paramAction = null)
        {
            int rowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        paramAction?.Invoke(cmd);
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                    sEQConnection.write(LogCategory.Warning, ex.StackTrace);
                    sEQConnection.write(LogCategory.Warning, ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    sEQConnection.write(LogCategory.Warning, ex.StackTrace);
                    sEQConnection.write(LogCategory.Warning, ex.Message);
                }
            }

            return rowsAffected;
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

        public void ExecuteReader(string procedureName, Action<SqlCommand> parameterSetter, Action<SqlDataReader> readerHandler)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    parameterSetter(cmd);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        readerHandler(reader);
                    }
                }
            }

        }
    }
}
