using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public delegate void OrderAdded();
    public class Order
    {
        public event OrderAdded OnAdded;
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public void Add()
        {
            Console.WriteLine("Adding Order...");
            if (OnAdded != null)
                OnAdded();
        }
    }


}
