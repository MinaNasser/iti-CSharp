namespace ValueType
{
    public enum UserOption : Byte
    {
        Add =1,
        Sub = 2,
        Mul = 3,
        Div = 4,
        Mod = 5,
        Exit=6
    }


    public struct Employee
    {
        public string Name;
        public int age;
        public Employee(string s, int a)
        {
            Name = s;
            age = a;

        }
        public void Display()
        {
            Console.WriteLine($"Name : {this.Name} , Age : {this.age}");
        }
             
    }
}
