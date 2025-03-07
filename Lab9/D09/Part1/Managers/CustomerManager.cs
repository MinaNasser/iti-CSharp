using PartII.Mdoels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace PartII.Managers
{
    public class CustomerManager : BaseManager<Customer>
    {
        class Enumerator : IEnumerator
        {
            private int top = 0;
            private List<Customer> list;
            public Enumerator(List<Customer> _list)
            {
                list = _list;
            }
            public object Current { get { return list[top -1]; } }
            public bool MoveNext()
            {
                if (top < list.Count)
                {
                    top++;
                    return true;
                }
                else return false;
            }
            public void Reset()
            {
                top = 0;
            }
        }
        public IEnumerator GetEnumerator ()
        {
            return new  Enumerator(Items);
        }
        public Customer this[int _index]
        {
            get { return Items[_index]; }
            set { Items[_index] = value; }
        }
        public CustomerManager(List<Customer> _customers) : base(_customers)
        { }
    }
}
