using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDataProvider
    {
         IList<Customer> customers { get; set; }
         IList<Order> orders { get; set; }
    }
}
