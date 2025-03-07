using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models

{
    public enum OrderStatus
    {
        Pending,
        Completed,
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public OrderStatus Status { get; set; } 
        public DateTime UpdatedAt { get; set; }  
        public DateTime CreatedAt { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
