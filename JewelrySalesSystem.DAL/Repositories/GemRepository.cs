using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

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
            IQueryable<Gem> gemsQuery = _dbSet
                .Include(g => g.GemPrice);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                gemsQuery = gemsQuery.Where(c =>
                    c.GemName.Contains(searchTerm) ||
                    c.Origin.Contains(searchTerm));
            }

            if (sortOrder?.ToLower() == "desc")
            {
                gemsQuery = gemsQuery.OrderByDescending(GetSortProperty(sortColumn));
            }
            else
            {
                gemsQuery = gemsQuery.OrderBy(GetSortProperty(sortColumn));
            }

            var gems = await PaginatedList<Gem>.CreateAsync(gemsQuery, page, pageSize);

            return gems;
        }

        private static Expression<Func<Gem, object>> GetSortProperty(string? sortColumn)
        => sortColumn?.ToLower() switch
        {
            "name" => gem => gem.GemName,
            //"dob" => gem => gem.DoB,
            _ => gem => gem.GemId
        };

        public async Task<Gem?> GetByIdWithIncludeAsync(int id)
        {
            var result = await _dbSet
                .Include(g => g.GemPrice)             
                .FirstOrDefaultAsync(g => g.GemId == id);

            if (result == null) return null;
            return result;
        }
    }
}
