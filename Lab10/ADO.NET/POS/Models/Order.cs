using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.model
{
    public enum orderStatus
    {
        pending,
        completed,
        cancelled
    }
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }



    }
}
