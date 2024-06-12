namespace JewelrySalesSystem.BAL.Models.MaterialPriceList
{
    public class CreateMaterialPriceList
    {
        public int MaterialId { get; set; } 
        public float BuyPrice { get; set; }
        public float SellPrice { get; set; }
        public DateTime EffDate { get; set; }
    }
}
