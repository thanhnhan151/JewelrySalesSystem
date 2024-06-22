namespace JewelrySalesSystem.BAL.Models.Invoices
{
    public class InvoiceItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public float ProductPrice { get; set; }
    }
}
