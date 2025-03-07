using System;

namespace PartI
{
    public interface ISortable<T>
    {
        int IsSmallerThan(T item);
    }

    public class SortHelper<T> where T : ISortable<T>
    {
        public static void Sort(T[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j].IsSmallerThan(arr[i]) == -1)
                    {
                        T temp = arr[j];
                        arr[j] = arr[i];
                        arr[i] = temp;
                    }
                }
            }
        }
    }

    public class Number : ISortable<Number>
    {
        public int Value { get; set; }

        public Number(int value)
        {
            Value = value;
        }

        public int IsSmallerThan(Number item)
        {
            if (this.Value < item.Value) return -1;
            if (this.Value > item.Value) return 1;
            return 0;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }


     public class Employee : ISortable<Employee>
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }

        public Employee(string name, decimal salary)
        {
            Name = name;
            Salary = salary;
        }

        // Sorting based on Salary (Ascending order)
        public int IsSmallerThan(Employee other)
        {
            if (this.Salary < other.Salary) return -1;
            if (this.Salary > other.Salary) return 1;
            return 0;
        }

        public override string ToString()
        {
            return $"{Name} (${Salary})";
        }
    }



}
