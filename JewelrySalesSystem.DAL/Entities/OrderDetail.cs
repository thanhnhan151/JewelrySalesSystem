using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem.DAL.Entities
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public float PurchaseTotal { get; set; }
        public float PerDiscount { get; set; }

        // Order
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;

        // Buy Invoice
        public int? BuyInvoiceId { get; set; }
        public virtual BuyInvoice BuyInvoice { get; set; } = null!;
    }
}
