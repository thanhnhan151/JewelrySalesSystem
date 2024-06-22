namespace JewelrySalesSystem.BAL.Models.Invoices
{
    public class CreateInvoiceRequest
    {
        public string InvoiceType { get; set; } = string.Empty;
        public float Total { get; set; }
        public float PerDiscount { get; set; }
        public float TotalWithDiscount { get; set; }

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
