using JewelrySalesSystem.BAL.Models.ProductTypes;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IProductTypeService
    {
        Task<GetProductTypeResponse?> GetByIdAsync(int id);

        Task<List<GetProductTypeResponse>> GetAllAsync();
    }
}
