using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class BuyInvoiceRepository : GenericRepository<BuyInvoice>, IBuyInvoiceRepository
    {
        public BuyInvoiceRepository(JewelryDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<List<BuyInvoice>> GetAllAsync() => await _dbSet.Include(o => o.Items).ToListAsync();

        public async Task<BuyInvoice?> GetByIdWithInclude(int id) => await _dbSet.Include(o => o.Items).FirstOrDefaultAsync(o => o.BuyInvoiceId == id);
    }
}
