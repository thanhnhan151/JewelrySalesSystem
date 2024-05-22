using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem.DAL.Entities
{
    [Table("Brand")]
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        public string BrandName { get; set; } = string.Empty;

        // Products
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
