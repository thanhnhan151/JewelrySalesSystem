namespace JewelrySalesSystem.BAL.Models.Invoices
{
    public class CreatePurchaseInvoiceRequest
    {
        // Customer
        public string CustomerName { get; set; } = string.Empty;

        // User
        public int UserId { get; set; }

        //public float Weight { get; set; }

        public virtual ICollection<float> Weights {  get; set; } = new List<float>();

        public float Total { get; set; }

        // Invoice Details
        public virtual ICollection<int> InvoiceDetails { get; set; } = new List<int>();
    }
}
