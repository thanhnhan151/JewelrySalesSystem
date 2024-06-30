using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class ProductMaterialRepository : GenericRepository<ProductMaterial>, IProductMaterialRepository
    {
        public ProductMaterialRepository(JewelryDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<float> GetProductMaterialWeightAsync(int materialId)
        {
            var result = await _dbSet.FirstOrDefaultAsync(m => m.MaterialId == materialId);
            if (result == null)
            {
                return 1;
            }
            return result.Weight;
        }
    }
}
