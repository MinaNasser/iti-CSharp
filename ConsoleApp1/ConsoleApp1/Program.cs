using App;
using ConsoleApp1;

namespace app
{
    
    public  class Program
    { 
        
        public static int Main()
        {
            
            List<Employee> employees = new List<Employee>()
            {
                new Employee(){Id= 1 , Name ="Mina", Salary=50000},
                new Employee(){Id= 2 , Name ="Mina", Salary=50000},
                new Employee(){Id= 3 , Name ="Ali", Salary=50000},
                new Employee(){Id= 4 , Name ="Mohamed", Salary=50000},
                new Employee(){Id= 5 , Name ="Marwa", Salary=50000},
                new Employee(){Id= 6 , Name ="Alaa", Salary=50000},

            };
            var Query =employees.IWhere(e=>e.Id>1).ISelect(e=>new { Emp_Id = e.Id, Emp_Name = e.Name, Emp_Salary = e.Salary } ).ToList();

            ;
            employees.Add(new Employee() { Id = 10, Name = "Test", Salary = 100000000 });
            foreach (var employee in Query)
            {
                Console.Write(
                    $"""
                        ID : {employee.Emp_Id},
                        Name : {employee.Emp_Name}, 
                        Salary :{employee.Emp_Salary}
                         
                    """

                    );
                Console.WriteLine(String.Empty);
                Console.WriteLine("###########################");

            }



            return 0;
             
        }
    }
}
