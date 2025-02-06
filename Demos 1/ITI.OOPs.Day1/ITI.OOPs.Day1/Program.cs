using System;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace ITI.OOPs.Day1
{
    class Program
    {
        static int Main()
        {
            SqlConnection con = null;
            try
            {

                con = new SqlConnection();
                con.ConnectionString = "Data Source=.; Initial Catalog=SWDF_Q3; Integrated Security = True";

                con.Open();
            }
            catch(Exception error)
            {
                Console.WriteLine( error.Message);
                Console.WriteLine( error.StackTrace);
                Console.WriteLine("Database Server Error");
            }


            Console.WriteLine(  "End Of Program");
            con.Close();

            return 0;
        }
    }
}