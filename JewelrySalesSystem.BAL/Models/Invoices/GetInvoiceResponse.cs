using System.Runtime.CompilerServices;

namespace JewelrySalesSystem.BAL.Models.Invoices
{
    public class GetInvoiceResponse
    {
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; }
        public bool InvoiceType { get; set; }

        // Customer
        public string CustomerName { get; set; } = string.Empty;

        // User
        public string UserName { get; set; } = string.Empty;

        // Warranty
        public string Warranty { get; set; } = string.Empty;

        // Invoice Details
        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();

        public float Total { get; set; }
    }
}
