using JewelrySalesSystem.BAL.Models.Orders;

namespace JewelrySalesSystem.BAL.Models.BuyInvoices
{
    public class CreateUpdateBuyInvoiceRequest
    {
        public string CustomerName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        // Order Details
        public virtual ICollection<OrderItem> Items { get; set; } = [];
    }
}
