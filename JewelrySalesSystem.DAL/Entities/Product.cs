using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem.DAL.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int PercentPriceRate { get; set; }
        public float ProductionCost { get; set; }
        public bool Status { get; set; }
        public string FeaturedImage { get; set; } = string.Empty;
        public string MaterialType { get; set; } = string.Empty;
        public float Weight { get; set; }
        // Category
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        // Product Type
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; } = null!;

        // Gender
        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; } = null!;

        // Colour
        public int ColourId { get; set; }
        public virtual Colour Colour { get; set; } = null!;

        // Invoice Details
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

        // Product Gems
        public virtual ICollection<ProductGem> ProductGems { get; set; } = new List<ProductGem>();

        // Product Materials
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();
    }
}
