using PartII.Mdoels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartII.Managers
{
    public class BaseManager<T> where T : BaseModel
    {
        protected List<T> Items;
        public BaseManager(List<T> _items)
        {
            Items = _items;
        }

        public List<T> Get()
        {
            return Items;
        }
        public T Get(int _id)
        {
            foreach (T t in Items)
            {
                if (t.Id == _id)
                    return t;
            }
            return null;
        }
        public void Add(T _item)
        {
            Items.Add(_item);
        }
    }
}
