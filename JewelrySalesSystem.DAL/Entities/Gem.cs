using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem.DAL.Entities
{
    [Table("Gem")]
    public class Gem
    {
        [Key]
        public int GemId { get; set; }
        public string GemName { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public float CaratWeight { get; set; }
        public string Colour { get; set; } = string.Empty;
        public string Clarity { get; set; } = string.Empty;
        public string Cut { get; set; } = string.Empty;
        public bool Status { get; set; }

        // Gem Price List
        public virtual GemPriceList GemPrice { get; set; } = null!;

        // Product Gems
        public virtual ICollection<ProductGem> ProductGems { get; set; } = new List<ProductGem>();
    }
}
