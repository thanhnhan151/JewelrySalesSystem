using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.DAL.Interfaces
{
    public interface IColourRepository : IGenericRepository<Colour>
    {
        Task<List<Colour>> GetAllAsync();
    }
}
