using Data.Providers;
using Models;
using System;
using System.Collections.Generic;

namespace Managers
{
    public class InvoiceManager
    {
        private readonly InvoiceProvider _invoiceProvider;

        public InvoiceManager(InvoiceProvider invoiceProvider)
        {
            _invoiceProvider = invoiceProvider ?? throw new ArgumentNullException(nameof(invoiceProvider));
        }

        public void AddInvoice(Invoice invoice)
        {
            _invoiceProvider.SaveInvoice(invoice);
        }

        public List<Invoice> GetAllInvoices()
        {
            return _invoiceProvider.GetAllInvoices();
        }

        public Invoice GetInvoiceByOrderId(int orderId)
        {
            return _invoiceProvider.GetInvoiceByOrderId(orderId);
        }
    }
}
