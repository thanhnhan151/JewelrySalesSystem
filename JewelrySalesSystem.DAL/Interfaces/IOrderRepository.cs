using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.DAL.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetByIdWithInclude(int id);
        Task DeleteAsync(int id);
        Task ChangeOrderStatusAsync(int id);
        Task CancelOrderAsync(int id);
    }
}
