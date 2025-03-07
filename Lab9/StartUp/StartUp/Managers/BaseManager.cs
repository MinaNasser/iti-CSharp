using StartUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StartUp.Managers
{
    public class BaseManager<T> where T : BaseModel
    {
        protected readonly List<T> Items;

        public BaseManager(List<T> _items)
        {
            Items = _items ?? new List<T>();
        }

        public List<T> Get()
        {
            return Items;
        }

        public T Get(int _id)
        {
            return Items.FirstOrDefault(t => t.Id == _id); 
        }

        public void Add(T _item)
        {
            Items.Add(_item);
        }

        public bool Remove(int _id)
        {
            var item = Get(_id);
            if (item != null)
            {
                Items.Remove(item);
                return true;
            }
            return false;
        }

        public bool Update(T _item)
        {
            var existingItem = Get(_item.Id);
            if (existingItem != null)
            {
                int index = Items.IndexOf(existingItem);
                Items[index] = _item;
                return true;
            }
            return false;
        }
    }
}
