namespace JewelrySalesSystem.BAL.Models.ProductTypes
{
    public class GetProductTypeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<ProductAndProductTypeResponse> Products { get; set; } = [];
    }
}
