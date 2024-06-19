using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(JewelryDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<List<Order>> GetAllAsync() => await _dbSet
            .Include(o => o.OrderDetails)
            .Where(o => !o.Status)
            .ToListAsync();

        public async Task<Order?> GetByIdWithInclude(int id) => await _dbSet.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.OrderId == id);

        public async Task DeleteAsync(int id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(o => o.OrderId == id);

            if (result != null)
            {
                result.Status = true;
            }
        }

        public async Task ChangeOrderStatusAsync(int id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(o => o.OrderId == id);

            if (result != null) ChangeStatus(result);
        }

        private static void ChangeStatus(Order order)
        {
            if (order.OrderStatus.Equals("Pending"))
            {
                order.OrderStatus = "Processing";
            }
            else if (order.OrderStatus.Equals("Processing"))
            {
                order.OrderStatus = "Delivered";
            }
        }

        public async Task CancelOrderAsync(int id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(o => o.OrderId == id);

            if (result != null) result.OrderStatus = "Cancelled";
        }
    }
}
