using Data.Connections;
using Data.Interfaces;
using Models;
using System.Collections.Generic;

namespace Data.Providers
{
    public class SQLServerDataProvider : IDataProvider
    {
        public IList<Customer> customers { get; set; }
        public IList<Order> orders { get; set; }

        private readonly SQLConnectionHelper _dbHelper;

        public SQLServerDataProvider()
        {
            _dbHelper = new SQLConnectionHelper(); // ✅ إنشاء `SQLConnectionHelper`
            customers = new CustomerProvider(_dbHelper); // ✅ تمرير `dbHelper`
            orders = new OrderProvider(_dbHelper); // ✅ تمرير `dbHelper`
        }
    }
}
