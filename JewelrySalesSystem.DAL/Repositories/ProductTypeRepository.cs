using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(JewelryDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<ProductType?> GetAllProductsByProductTypeIdAsync(int productTypeId)
        {
            return await _dbSet.Include(pt => pt.Products)
                                    .ThenInclude(p => p.Gender)
                            .Include(pt => pt.Products)
                                .ThenInclude(p => p.Colour)
                            .Include(pt => pt.Products)
                                 .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(pt => pt.Id == productTypeId);
        }
    }
}
