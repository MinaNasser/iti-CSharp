using System;
using StartUp.Models;
using StartUp.Managers;
using PartII.Data;
using System.Collections.Generic;

namespace StartUp
{
    class Program
    {
        static DataProvider dataProvider = new DataProvider();
        static CustomerManager customerManager;
        static OrderManager orderManager;

        static void Main(string[] args)
        {

            //SeedData();

            customerManager = new CustomerManager(dataProvider.Customers);
            orderManager = new OrderManager(dataProvider.Orders);

            MenuHandler menuHandler = new MenuHandler(customerManager, orderManager);
            menuHandler.ShowMenu();
        }

        //static void SeedData()
        //{
        //    // Adding some dummy customers and orders
        //    dataProvider.Customers.Add(new Customer(1, "Mina"));
        //    dataProvider.Customers.Add(new Customer(2, "Ahmed"));
        //    dataProvider.Orders.Add(new Order(1, "Order 1", 1));
        //    dataProvider.Orders.Add(new Order(2, "Order 2", 2));
        //}
    }
}
