

using ITI.OOPs.Managers;
using ITI.OOPs.Models;
using System;
using System.Collections.Generic;

namespace ITI.OOPs.Presentation
{
    class Program
    {
        static int Main()
        {
            //Console.WriteLine("Enter Trainee Info :"); ;
            //Console.WriteLine("Enter ID  :");
            ////int ID = int.Parse(Console.ReadLine());
            ////int ID = Convert.ToInt32(Console.ReadLine());
            //int ID;
            //bool DOneOrNor = int.TryParse(Console.ReadLine(), out ID);


            //switch (DOneOrNor)
            //{
            //    case false:
            //        {
            //            Console.WriteLine("Invalid Value");
            //            return -1;
            //        }
            //        break;
            //    case true:
            //        {

            //        }
            //        break;
            //    default:
            //        {

            //        }
            //        break;
            //}


            //string Name = Console.ReadLine();
            //Console.WriteLine("1- Male, 2-Female");
            //Geneder Geneder = (Geneder)(byte.Parse(Console.ReadLine()));

            //Trainee T2;
            //T2.Id = 10;
            //T2.Name = Name;
            //T2.Gender = Geneder;
            //;

            //T2.Display();

            //long MaxValue = long.MaxValue;
            //int ReturnedValue = 0;
            //unchecked
            //{
            //    ReturnedValue = (int)MaxValue;
            //}

            //Console.WriteLine(ReturnedValue);


            //TraineeManager obj = new TraineeManager();

            ////int FirstTraineeID  = (obj.Get()[0]).Id;
            //Trainee MyTrainee = new Trainee();


            //Trainee Copy = MyTrainee;
            //MyTrainee.Id = 1;

            //Copy.Display();

            //MyTrainee.Display();

            TraineeManager obj = new TraineeManager();
            List<Trainee> list = obj.Get();

            //for (int i = 0; i < list.Count; i++)
            //    (list[i]).Display();

            //int i = 0; 
            //while(i < list.Count)
            //{
            //    (list[i]).Display();
            //    i++;
            //}

            //int i = 0; 
            //do
            //{
            //    (list[i]).Display();
            //    i++;

            //}while(i < list.Count);


            var temp  = list.GetEnumerator();
            while(temp.MoveNext())
            {
                ((Trainee)(temp.Current)).Display();
            }



            int z = "";

            //foreach (Trainee t in list)
            //{
            //    t = new Trainee();
            //    t.Display();
            //}


            //T.Display();
            //Int32 FirstTraineeID = (T).Id;
            //int CopyOfID = FirstTraineeID;
            //FirstTraineeID++;
            //Console.WriteLine($"CopyOfID =  {CopyOfID}");
            //Console.WriteLine($"Main ID =  {FirstTraineeID}");
            ///*

            //    C# Keyword
            //    char
            //    byte     Byte
            //    sbyte    SByte
            //    short    
            //    ushort
            //    int      Int32
            //    uint     UInt32
            //    long     Int64
            //    ulong 
            //    float    Signle
            //    double   Double
            //    decimal  Decimal
            // */


            return 0;
        }
    }
}