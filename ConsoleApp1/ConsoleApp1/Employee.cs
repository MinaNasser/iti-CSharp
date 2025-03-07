using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Salary { get; set; }
        public int EmployeeId { get; set; } 
        public override string ToString()
        {
            return $"""
                    ID : {Id},
                    Name : {Name}, 
                    Salary :{Salary}
                """;
        }
    }
}
