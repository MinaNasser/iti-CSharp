using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System;

namespace UI
{
    

    public class InvoicePrinter
    {
        public void Print(Invoice invoice)
        {
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine("                 INVOICE                    ");
            Console.WriteLine("============================================");
            Console.WriteLine($" Invoice Number: {invoice.InvoiceNumber}");
            Console.WriteLine($" Order ID: {invoice.OrderId}");
            Console.WriteLine($" Customer ID: {invoice.CustomerId}");
            Console.WriteLine($" Total Amount: {invoice.TotalAmount:C}");
            Console.WriteLine($" Status: {invoice.Status}");
            Console.WriteLine($" Created At: {invoice.CreatedAt}");
            Console.WriteLine("============================================");
            Console.WriteLine("        Thank you for your purchase!        ");
            Console.WriteLine("============================================");
        }
    }

}
