using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem.DAL.Entities
{
    [Table("BuyInvoice")]
    public class BuyInvoice
    {
        [Key]
        public int BuyInvoiceId { get; set; }
        public string InvoiceType { get; set; } = "Buy";
        public string CustomerName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public bool Status { get; set; } = false;

        // Order Details
        public virtual ICollection<OrderDetail> Items { get; set; } = [];
    }
}
