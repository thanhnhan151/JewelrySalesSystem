using JewelrySalesSystem.BAL.Models.Orders;

namespace JewelrySalesSystem.BAL.Models.BuyInvoices
{
    public class CreateUpdateBuyInvoiceRequest
    {
        public int BuyInvoiceId { get; set; }
        public string InvoiceType { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public float Total { get; set; }

        // Order Details
        public virtual ICollection<OrderItem> Items { get; set; } = [];
    }
}
