using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartII.Mdoels
{
    public class Order : BaseModel
    {
        public decimal Price { get; set; }
        public Customer Customer { get; set; }
        public override string ToString()
        {
            return $"ID: {Id}, Price: {Price}, {Customer}";
        }
    }
}
