

using System;
using System.Collections;
using System.Collections.Generic;
using PartI;
using PartII.Data;
using PartII.Managers;
using PartII.Mdoels;

public class Program
{
    static int Main()
    {
        Number[] numbers = { 
            new Number(3),
            new Number(1), 
            new Number(4),
            new Number(2)
        };

        Console.WriteLine("Before Sorting:");
        foreach (var num in numbers) Console.Write(num + " ");

        SortHelper<Number>.Sort(numbers);

        Console.WriteLine("\nAfter Sorting:");
        foreach (var num in numbers) Console.Write(num + " ");


        numbers[0].Value = 1;

        Console.WriteLine();
        Console.WriteLine();
        Employee[] employees =
            {
                new Employee("Alice", 5000),
                new Employee("Bob", 3000),
                new Employee("Charlie", 7000),
                new Employee("David", 6000)
            };

        Console.WriteLine("Before Sorting:");
        foreach (var emp in employees) Console.WriteLine(emp);

        SortHelper<Employee>.Sort(employees);

        Console.WriteLine("\nAfter Sorting (by Salary Ascending):");
        foreach (var emp in employees) Console.WriteLine(emp);


        return 0;
        }
    }


/*
 DataProvider db = new DataProvider();
        Customer c = new Customer() { Id = 1, Name = "Ahmed" };
        CustomerManager mngr = new CustomerManager(db.Customers);
        mngr.Add(c);

        //mngr[0] = new Customer() { Id  = 2, Name= "Test"};

        //Console.WriteLine(mngr[0]);

        //List<Customer> lst = mngr.Get();
        //for (int i =0; i < lst.Count; i++)
        //    Console.WriteLine(lst[i]);


        //foreach(Customer item in mngr)
        //    Console.WriteLine(item);


        IEnumerator enumerator =  mngr.GetEnumerator();
        while (enumerator.MoveNext())
        {
            Console.WriteLine(enumerator.Current);
        }
 
 
 */


/// Task 1 
/// 


/*
 Stack<int> sI = new Stack<int>();
        sI.Push(1);
        sI.Push(2);
        sI.Push(3);
        sI.Push(4);
        sI.Push(5);

        while (sI.Count > 0) {
            Console.Write($"{sI.Pop()}\t");
        }
        Console.WriteLine();
        Stack<Customer> customers = new Stack<Customer>();
        customers.Push(new Customer()
        {
            Id = 1,
            Name = "Test",
        });
        customers.Push(new Customer()
        {
            Id= 2,
            Name = "Test2",
        });

        foreach (Customer customer in customers)
        {
            Console.WriteLine(customer.Name);
            Console.WriteLine(customer.Id);
        }


        Stack stack = new Stack();
        //Adding item to the stack using the push method
        stack.Push(10);
        stack.Push("Hello");
        stack.Push(3.14f);
        stack.Push(true);
        stack.Push(67.8);
        stack.Push('A');
        //Printing the stack items using foreach loop
        foreach (object item in stack)
        {
            Console.WriteLine(item);
        }
 
 */


