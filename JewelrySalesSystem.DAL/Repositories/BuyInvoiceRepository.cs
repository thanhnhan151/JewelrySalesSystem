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

        public async Task CancelBuyInvoiceAsync(int id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(o => o.BuyInvoiceId == id);

            if (result != null)
            {
                result.BuyInvoiceStatus = "Cancelled";

                _dbSet.Update(result);
            }            
        }

        public async Task ChangeBuyInvoiceStatusAsync(int id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(o => o.BuyInvoiceId == id);

            if (result != null)
            {
                ChangeStatus(result);

                _dbSet.Update(result);
            }              
        }

        public async Task<List<BuyInvoice>> GetAllAsync() => await _dbSet.Include(o => o.Items).ToListAsync();

        public async Task<BuyInvoice?> GetByIdWithInclude(int id) => await _dbSet.Include(o => o.Items).FirstOrDefaultAsync(o => o.BuyInvoiceId == id);

        private static void ChangeStatus(BuyInvoice buyInvoice)
        {
            if (buyInvoice.BuyInvoiceStatus.Equals("Pending"))
            {
                buyInvoice.BuyInvoiceStatus = "Processing";
            }
            else if (buyInvoice.BuyInvoiceStatus.Equals("Processing"))
            {
                buyInvoice.BuyInvoiceStatus = "Delivered";
            }
        }
    }
}
