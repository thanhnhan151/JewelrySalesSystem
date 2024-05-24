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

        public async Task<PaginatedList<Gem>> PaginationAsync(
            string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize)
        {
            IQueryable<Gem> gemsQuery = _dbSet;

            //if (!string.IsNullOrWhiteSpace(searchTerm))
            //{
            //    gemsQuery = gemsQuery.Where(c =>
            //        c.FullName.Contains(searchTerm) ||
            //        c.PhoneNumber.Contains(searchTerm) ||
            //        c.Email.Contains(searchTerm));
            //}

            //if (sortOrder?.ToLower() == "desc")
            //{
            //    gemsQuery = gemsQuery.OrderByDescending(GetSortProperty(sortColumn));
            //}
            //else
            //{
            //    gemsQuery = gemsQuery.OrderBy(GetSortProperty(sortColumn));
            //}

            var gems = await PaginatedList<Gem>.CreateAsync(gemsQuery, page, pageSize);

            return gems;
        }
    }
}
