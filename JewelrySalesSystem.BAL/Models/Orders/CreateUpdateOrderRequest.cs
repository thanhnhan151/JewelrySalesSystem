namespace JewelrySalesSystem.BAL.Models.Orders
{
    public class CreateUpdateOrderRequest
    {
        public string CustomerName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Warranty { get; set; } = string.Empty;

        // Order Details
        public ICollection<OrderItem> OrderDetails { get; set; } = [];
    }
}
