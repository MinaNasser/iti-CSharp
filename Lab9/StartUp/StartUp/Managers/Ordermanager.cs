using StartUp.Managers;
using StartUp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StartUp.Managers
{
    public class OrderManager : BaseManager<Order>
    {
        public class Enumlator : IEnumerator {
            private int top = 0;
            private List<Order> order;
            public Enumlator(List<Order> _order) { 
                order = _order;
            }
            public object Current { get { return order[top - 1]; } }
            public bool MoveNext()
            {
                if (top < order.Count)
                {
                    top++;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                top = 0;
            }
        
        
        }


        public IEnumerator GetEnumerator()
        {
            return new Enumlator(Items);
        }
        public OrderManager(List<Order> _orders) : base(_orders ?? new List<Order>()) { }
        public List<Order> GetByCustomerId(int customerId)
        {
            return Items.Where(order => order.CustomerId == customerId).ToList();
        }
    }
}
