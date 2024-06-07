using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.Invoices
{
    public class CreateInvoiceRequest
    {
        public bool Status { get; set; }
        public bool InvoiceType { get; set; }

        // Customer
        public int CustomerId { get; set; }

        // User
        public int UserId { get; set; }

        // Warranty
        public int WarrantyId { get; set; }

        // Invoice Details
        public virtual ICollection<int> InvoiceDetails { get; set; } = new List<int>();
    }
}
