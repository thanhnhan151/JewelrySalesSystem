namespace JewelrySalesSystem.BAL.Models.Materials
{
    public class GetMaterialResponse
    {
        public string MaterialName { get; set; } = string.Empty;
        public MaterialPrice MaterialPrice { get; set; } = null!;
    }
}
