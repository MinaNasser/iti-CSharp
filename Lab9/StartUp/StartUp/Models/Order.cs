using System;

namespace StartUp.Models
{
    public class Order : BaseModel
    {

        public int CustomerId { get; set; } 


        public Order() { }

        public Order(int id, string name, int customerId)
        {
            Id = id;
            Name = name;
            CustomerId = customerId;
        }

        public override string ToString()
        {
            return $"Order ID: {Id} - Name: {Name} - Customer ID: {CustomerId}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Order other)
                return Id == other.Id && Name == other.Name && CustomerId == other.CustomerId;
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, CustomerId);
        }
    }
}
