using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.Extensions.Logging;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class WarrantyRepository : GenericRepository<Warranty>, IWarrantyRepository
    {
        public WarrantyRepository(
            JewelryDbContext context, 
            ILogger logger) : base(context, logger)
        {
        }

        public async Task<PaginatedList<Warranty>> PaginationAsync(
            string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize)
        {
            IQueryable<Warranty> warrantiesQuery = _dbSet;

            //if (!string.IsNullOrWhiteSpace(searchTerm))
            //{
            //    warrantiesQuery = warrantiesQuery.Where(c =>
            //        c.FullName.Contains(searchTerm) ||
            //        c.PhoneNumber.Contains(searchTerm) ||
            //        c.Email.Contains(searchTerm));
            //}

            //if (sortOrder?.ToLower() == "desc")
            //{
            //    warrantiesQuery = warrantiesQuery.OrderByDescending(GetSortProperty(sortColumn));
            //}
            //else
            //{
            //    warrantiesQuery = warrantiesQuery.OrderBy(GetSortProperty(sortColumn));
            //}

            var warranties = await PaginatedList<Warranty>.CreateAsync(warrantiesQuery, page, pageSize);

            return warranties;
        }
    }
}
