namespace JewelrySalesSystem.BAL.Models.Orders
{
    public class GetOrderResponse
    {
        public int OrderId { get; set; }
        public string InvoiceType { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Warranty { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; }
        public float Total { get; set; }

        public ICollection<OrderItem> OrderDetails { get; set; } = [];
    }
}
