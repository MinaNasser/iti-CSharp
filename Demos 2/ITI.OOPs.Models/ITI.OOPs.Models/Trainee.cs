

using System;

namespace ITI.OOPs.Models
{
    public struct  Trainee
    {
        public int Id;
        public string Name;
        public Geneder Gender;
        public void Display()
        {
            Console.WriteLine(  $"Id: {Id}, Name:{Name}, Gender:{Gender}");
        }
    }
}