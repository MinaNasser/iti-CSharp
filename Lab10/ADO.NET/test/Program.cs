using Microsoft.Data.SqlClient;
using POS.model;
using System.Data;
using System;

using System;
using System.Linq;

namespace Test
{
    public class Program
    {
        public static void Main()
        {
        }
    }
}

//string connctionString = "Data Source =DB_PointOfSale.mssql.somee.com; " +
//          "Initial Catalog = DB_PointOfSale;" +
//          "User ID =Al5al_SQLLogin_1;" +
//          "Password =rmtgbk1xu2;" +
//          " TrustServerCertificate=True;" +
//          "Timeout=1";
//SqlConnection conn = new SqlConnection(connctionString);

//if (conn.State != ConnectionState.Open)
//    conn.Open();

//SqlCommand cmd = conn.CreateCommand();
//cmd.Connection = conn;
//cmd.CommandType = CommandType.Text;
//cmd.CommandText = "SELECT * FROM customers";

//SqlDataReader reader = cmd.ExecuteReader();

//List<Customer> list = new List<Customer>();
//while (reader.Read())
//{
//    int id = Convert.ToInt32(reader["c_id"]);
//    string name = Convert.ToString(reader["c_name"]);
//    string email = reader["c_email"].ToString();
//    string phone = reader["c_phone"].ToString();
//    list.Add(new Customer() { Id = id, Email = email, Name = name, Phone = phone });
//}
//conn.Close();
//foreach (Customer customer in list)
//{
//    Console.WriteLine(customer.Id);
//    Console.WriteLine(customer.Name);
//    Console.WriteLine(customer.Email);
//    Console.WriteLine(customer.Phone);
//}


//string message1 = "Happy Ramadan";
//string message2 = "ITI Aswan";

//PrintMessage(message1, message2);
//        }

//        private static void PrintMessage(string message1, string message2)
//{
//    Console.OutputEncoding = System.Text.Encoding.UTF8;

//    Console.ForegroundColor = ConsoleColor.Yellow;

//    message1 = $"** {message1} **";
//    message2 = $"** {message2} **";

//    int maxLength = Math.Max(message1.Length, message2.Length) + 8;

//    string border = string.Concat(Enumerable.Repeat("*", maxLength));


//    Console.WriteLine(border);
//    Console.ForegroundColor = ConsoleColor.Green;
//    Console.WriteLine(CenterText(message1, maxLength));
//    Console.WriteLine(CenterText(message2, maxLength));
//    Console.WriteLine(CenterText("* By Mina *", maxLength));
//    Console.ForegroundColor = ConsoleColor.Yellow;
//    Console.WriteLine(border);
//    //Console.ResetColor();
//    Console.ForegroundColor = ConsoleColor.Black;

//}

//private static string CenterText(string text, int width)
//{
//    int totalPadding = width - text.Length;
//    int leftPadding = totalPadding / 2;
//    int rightPadding = totalPadding - leftPadding;
//    return new string(' ', leftPadding) + text + new string(' ', rightPadding);
//}