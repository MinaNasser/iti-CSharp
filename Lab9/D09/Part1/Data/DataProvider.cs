using PartII.Mdoels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartII.Data
{
    public class DataProvider
    {
        public List<Customer> Customers { get; set; }
        public List<Order> Orders { get; set; }
        public DataProvider()
        {
            Customers = new List<Customer>();
            Orders = new List<Order>();
        }
    }
}
