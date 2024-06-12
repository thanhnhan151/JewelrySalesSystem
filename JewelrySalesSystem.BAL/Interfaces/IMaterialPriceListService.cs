using JewelrySalesSystem.BAL.Models.MaterialPriceList;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IMaterialPriceListService
    {
        Task<CreateMaterialPriceList> AddAsync (int id, CreateMaterialPriceList materialPriceList);
    }
}
