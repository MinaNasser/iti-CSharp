using System;

namespace Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "pending"; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
