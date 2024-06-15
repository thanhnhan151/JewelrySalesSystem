using JewelrySalesSystem.BAL.Models.ProductTypes;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IProductTypeService
    {
        Task<GetProductTypeResponse?> GetAllProductsByProductTypeIdAsync(int productTypeId); 
    }
}
