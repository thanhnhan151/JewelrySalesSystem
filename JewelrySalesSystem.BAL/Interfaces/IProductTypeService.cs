using JewelrySalesSystem.BAL.Models.Categories;
using JewelrySalesSystem.BAL.Models.ProductType;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IProductTypeService
    {
        Task<List<GetAllProductType>> GetAllAsync();

        Task<GetProductTypeById> GetProductTypeByIdAsync(int id);
    }
}
