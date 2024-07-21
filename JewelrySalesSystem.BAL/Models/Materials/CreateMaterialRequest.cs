namespace JewelrySalesSystem.BAL.Models.Materials
{
    public class CreateMaterialRequest
    {
        public string MaterialName { get; set; } = string.Empty;
        public CreateMaterialPrice MaterialPrice { get; set; } = null!;
    }
}
