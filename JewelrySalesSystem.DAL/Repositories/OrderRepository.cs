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

        public async Task<List<Order>> GetAllAsync() => await _dbSet.Include(o => o.OrderDetails).ToListAsync();

        public async Task<Order?> GetByIdWithInclude(int id) => await _dbSet.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.OrderId == id);
    }
}
