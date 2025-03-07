using System;

namespace StartUp.Models
{
    public class Customer : BaseModel
    {
        public Customer() { }

        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"Customer ID: {Id} - Name: {Name}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Customer other)
                return Id == other.Id && Name == other.Name;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
