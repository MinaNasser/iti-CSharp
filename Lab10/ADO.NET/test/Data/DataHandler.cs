using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Data
{
    public class DataHandler
    {
        private string connctionString = "Data Source =DB_PointOfSale.mssql.somee.com; " +
            "Initial Catalog = POS;" +
            "User ID =Al5al_SQLLogin_1;" +
            "Password =rmtgbk1xu2;" +
            " TrustServerCertificate=True;" +
            "Timeout=1";
        SqlConnection connction = new SqlConnection();


    }
}
