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
        public int CategoryId { get; set; }

        // Product Type
        public int ProductTypeId { get; set; }

        // Gender
        public int GenderId { get; set; }

        // Colour
        public int ColourId { get; set; }

        // Invoice Details

        // Product Gems
        public ICollection<GemItem> Gems { get; set; } = new List<GemItem>();

        // Product Materials
        public ICollection<MaterialItem> Materials { get; set; } = new List<MaterialItem>();
    }
}
