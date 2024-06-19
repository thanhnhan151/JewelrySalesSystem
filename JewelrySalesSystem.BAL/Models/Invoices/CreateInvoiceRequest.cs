namespace JewelrySalesSystem.BAL.Models.Invoices
{
    public class CreateInvoiceRequest
    {
        public bool Status { get; set; }
        public bool InvoiceType { get; set; }

        // Customer
        public string CustomerName { get; set; } = string.Empty;

        // User
        public int UserId { get; set; }

        // Warranty
        public int WarrantyId { get; set; }

        // Invoice Details
        public virtual ICollection<int> InvoiceDetails { get; set; } = new List<int>();
    }
}
