using POS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public abstract class Service<T>
    {
        protected readonly DatabaseHelper dbHelper;

        protected Service(DatabaseHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public abstract void Add(T entity);
        public abstract void Update(T entity);
        public abstract void Delete(int id);
        public abstract List<T> GetAll();
        public abstract T GetById(int id);
    }

}
