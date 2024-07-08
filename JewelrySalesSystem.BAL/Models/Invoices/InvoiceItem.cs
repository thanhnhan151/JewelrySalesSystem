namespace JewelrySalesSystem.BAL.Models.Invoices
{
    public class InvoiceItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public float ProductPrice { get; set; }
    }
}
