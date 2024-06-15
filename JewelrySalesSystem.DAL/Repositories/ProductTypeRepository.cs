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

        public async Task<List<ProductType>> GetAllAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public async Task<ProductType> GetById(int id)
        {
            var result = await _context.ProductTypes.FindAsync(id);
            if(result == null)
            {
                throw new Exception($"Can't find product type with '{id}'");
            }
            return result;
        }
    }
}
