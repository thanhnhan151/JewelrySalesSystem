namespace JewelrySalesSystem.BAL.Models.Invoices
{
    public class InvoiceItem
    {
        public string ProductName { get; set; } = string.Empty;
        public float Total { get; set; }
        public float PerDiscount { get; set; }
    }
}
