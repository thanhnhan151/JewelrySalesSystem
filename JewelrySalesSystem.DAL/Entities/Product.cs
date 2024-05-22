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
        public float MaterialCost { get; set; }
        public float GemCost { get; set; }
        public float ProductionCost { get; set; }
        public bool Status { get; set; }
        public string FeaturedImage { get; set; } = string.Empty;
        public string FirstImage { get; set; } = string.Empty;
        public string SecondImage { get; set; } = string.Empty;

        // Category
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        // Brand
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; } = null!;

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

        // Warranty
        public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
    }
}
