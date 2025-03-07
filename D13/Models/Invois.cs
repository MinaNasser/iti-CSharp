using System;
using System.Linq;
using System.Collections.Generic;

namespace Models
{
    public class Invois
    {
        public void Print(Order order)
        {
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine("                 INVOICE                    ");
            Console.WriteLine("============================================");
            Console.WriteLine($" Date: {order.Date}");
            Console.WriteLine($" Customer: {order.Customer.Name}");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine(" ID   Name            Qty    Price     Total");
            Console.WriteLine("--------------------------------------------");

            decimal totalAmount = 0;
            foreach (var item in order.Items)
            {
                decimal itemTotal = item.Qty * item.Price;
                totalAmount += itemTotal;
                Console.WriteLine($"{item.Id,-4} {item.Item.Name,-15} {item.Qty,-6} {item.Price,-9:C} {itemTotal,-10:C}");
            }

            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($" Total Amount: {totalAmount,32:C}");
            Console.WriteLine("============================================");
            Console.WriteLine("        Thank you for your purchase!        ");
            Console.WriteLine("============================================");
            Console.ReadLine();
        }
    }
}
