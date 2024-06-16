using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class ColourRepository : GenericRepository<Colour>, IColourRepository
    {
        public ColourRepository(JewelryDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<List<Colour>> GetAllAsync() => await _dbSet.ToListAsync();
    }
}
