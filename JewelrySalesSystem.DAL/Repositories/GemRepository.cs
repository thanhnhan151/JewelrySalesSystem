using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.Extensions.Logging;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class GemRepository : GenericRepository<Gem>, IGemRepository
    {
        public GemRepository(
            JewelryDbContext context, 
            ILogger logger) : base(context, logger)
        {
        }

        public Task<PaginatedList<Gem>> PaginationAsync(
            string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
