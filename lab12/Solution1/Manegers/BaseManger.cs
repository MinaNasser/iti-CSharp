namespace Manegers
{
    public abstract class BaseManger<T>
    {
        public abstract void Add(T item);
        public abstract void Update(T item);
        public abstract void Remove(T item);
        public abstract T GetById(int id);
        public abstract IList<T> GetAll();
        public abstract void Clear();
    }
}

