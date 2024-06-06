using System.Runtime.CompilerServices;

namespace JewelrySalesSystem.BAL.Models.Invoices
{
    public class GetInvoiceResponse
    {
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; }
        public bool InvoiceType { get; set; }

        // Customer
        public int CustomerId { get; set; }

        // User
        public int UserId { get; set; }

        // Warranty
        public int WarrantyId { get; set; }

        // Invoice Details
        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();

        public float Total { get; set; }
    }
}
