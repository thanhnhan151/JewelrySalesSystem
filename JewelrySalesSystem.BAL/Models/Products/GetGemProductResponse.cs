using JewelrySalesSystem.BAL.Models.Gems;

namespace JewelrySalesSystem.BAL.Models.Products
{
    public class GetGemProductResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string FeaturedImage { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public float CaratWeight { get; set; }
        public string Colour { get; set; } = string.Empty;
        public string Clarity { get; set; } = string.Empty;
        public string Cut { get; set; } = string.Empty;
        public GemPrice GemPrice { get; set; } = null!;

        // Product Type
        public string ProductType { get; set; } = string.Empty;
    }
}
