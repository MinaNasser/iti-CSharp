using System;

namespace StartUp.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public virtual string Name { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name}: Id = {Id}, Name = {Name}";
        }

        public override bool Equals(object obj)
        {
            if (obj is BaseModel other)
                return Id == other.Id;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
