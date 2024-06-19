using JewelrySalesSystem.BAL.Models.Orders;

namespace JewelrySalesSystem.BAL.Models.BuyInvoices
{
    public class GetBuyInvoiceResponse
    {
        public int BuyInvoiceId { get; set; }
        public string InvoiceType { get; set; } = string.Empty;
        public string BuyInvoiceStatus { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; } = [];
    }
}
