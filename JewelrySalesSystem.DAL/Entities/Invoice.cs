using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem.DAL.Entities
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; }
        public bool InvoiceType { get; set; }

        // Customer
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;

        // User
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        // Warranty
        public int WarrantyId { get; set; }
        public virtual Warranty Warranty { get; set; } = null!;

        // Invoice Details
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
    }
}
