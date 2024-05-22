using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem.DAL.Entities
{
    [Table("Warranty")]
    public class Warranty
    {
        [Key]
        public int WarrantyId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Product
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
    }
}
