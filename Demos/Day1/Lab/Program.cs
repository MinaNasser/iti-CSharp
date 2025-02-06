using System;
using ValueType;

namespace Task{
    
    class Program{
        static void Main(string[] args){
            
            
            /*try{
                Console.WriteLine("Enter 1st");
                int nm1 = int.Parse(Console.ReadLine()??"");
                Console.WriteLine("Enter 2nd");
                int nm2 = int.Parse(Console.ReadLine()??"");
                int res = nm1 / nm2;
                Console.WriteLine($"Resulte : {res}");
            }
            catch (FormatException){
                Console.WriteLine("Error: Please enter valid integers.");
            }
            catch (DivideByZeroException){
                Console.WriteLine("Error: Division by zero is not allowed.");
            }
            catch (Exception ex){
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            finally{ 
                Console.WriteLine("Execution completed.");
            }*/
            /*Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine($"Hello, {name}!");*/
            /*Employee employee = new Employee();
            employee.Name = "Test";
            employee.age = 1;
            employee.Display();*/
            /*UserOption op = 0;
            do {
                Console.WriteLine("Enter choose :");
                Console.WriteLine("1- ADD :");
                Console.WriteLine("2- Sub :");
                Console.WriteLine("3- Mul :");
                Console.WriteLine("4- Div :");
                Console.WriteLine("5- Mod :");
                Console.WriteLine("6- Exit :");
                op = (UserOption)int.Parse(Console.ReadLine()??"-1");
                if (op==UserOption.Add)
                {
                    Console.WriteLine("YOu Enter ADD");
                }else if (op == UserOption.Sub)
                {
                    Console.WriteLine("YOu Enter Sub");
                }
                else if (op == UserOption.Mul)
                {
                    Console.WriteLine("YOu Enter Mul");
                }
                else if (op == UserOption.Div)
                {
                    Console.WriteLine("YOu Enter Div");
                }
                else if (op == UserOption.Mod)
                {
                    Console.WriteLine("YOu Enter Mod");
                }
                else if (op == UserOption.Exit)
                {
                    Console.WriteLine("YOu Exit Now");
                }
                else 
                {
                    Console.WriteLine("Invalid ");
                }

            } while (op!=UserOption.Exit);*/
        }
    }
}
