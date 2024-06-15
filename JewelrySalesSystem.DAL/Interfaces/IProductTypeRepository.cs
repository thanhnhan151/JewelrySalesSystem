using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.DAL.Interfaces
{
    public interface IProductTypeRepository : IGenericRepository<ProductType>
    {
        Task<List<ProductType>> GetAllAsync();

        Task<ProductType> GetById(int id);
    }
}
