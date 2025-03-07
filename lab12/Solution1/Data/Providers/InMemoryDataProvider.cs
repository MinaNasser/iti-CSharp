using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Providers
{
    public class InMemoryDataProvider : IDataProvider
    {
        public IList<Customer> customers { get; set; }
        public IList<Order> orders { get; set; }
        public InMemoryDataProvider()
        {
            customers = new List<Customer>();
            orders = new List<Order>();

        }
    }
}
