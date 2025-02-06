using System;

namespace ITI.OOPs.Models
{
    public class Trainee
    {
        public int Id;
        public string Name;
        public void Display()
        {
            Console.WriteLine($"ID:{Id}, Name:{Name}");
        }
    }
}