using PartII.Mdoels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartII.Managers
{
    public class OrderManager : BaseManager<Order>
    {
        public OrderManager(List<Order> _orders) : base(_orders)
        { }
    }
}
