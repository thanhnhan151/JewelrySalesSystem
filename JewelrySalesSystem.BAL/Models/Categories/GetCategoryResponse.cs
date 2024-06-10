using JewelrySalesSystem.BAL.Models.Products;

namespace JewelrySalesSystem.BAL.Models.Categories
{
    public class GetCategoryResponse
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public ICollection<GetProductResponse> Products { get; set; } = [];
    }
}
