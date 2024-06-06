using JewelrySalesSystem.BAL.Models.Gems;
using JewelrySalesSystem.BAL.Models.Materials;

namespace JewelrySalesSystem.BAL.Models.Products
{
    public class GetProductResponse
    {
        public string ProductName { get; set; } = string.Empty;
        public int PercentPriceRate { get; set; }
        public float ProductionCost { get; set; }
        public bool Status { get; set; }
        public string FeaturedImage { get; set; } = string.Empty;

        // Category
        public string Category { get; set; } = string.Empty;

        // Product Type
        public string ProductType { get; set; } = string.Empty;

        // Gender
        public string Gender { get; set; } = string.Empty;

        // Colour
        public string Colour { get; set; } = string.Empty;

        // Product Gems
        public ICollection<GemItem> Gems { get; set; } = new List<GemItem>();

        // Product Materials
        public ICollection<MaterialItem> Materials { get; set; } = new List<MaterialItem>();
    }
}
