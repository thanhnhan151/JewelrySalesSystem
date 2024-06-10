
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.Extensions.Logging;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class GemPriceListRepository : GenericRepository<GemPriceList>, IGemPriceListRepository
    {
        public GemPriceListRepository(JewelryDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<GemPriceList> AddGemPrice(GemPriceList gemPrice)
        {
            gemPrice.EffDate = DateTime.Now;
            _context.GemPrices.Add(gemPrice);
            await _context.SaveChangesAsync();
            return gemPrice;
        }
    }
}
