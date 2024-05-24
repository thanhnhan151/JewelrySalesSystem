using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.Extensions.Logging;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class MaterialRepository : GenericRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(
            JewelryDbContext context, 
            ILogger logger) : base(context, logger)
        {
        }

        public async Task<PaginatedList<Material>> PaginationAsync(
            string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize)
        {
            IQueryable<Material> materialsQuery = _dbSet;

            //if (!string.IsNullOrWhiteSpace(searchTerm))
            //{
            //    materialsQuery = materialsQuery.Where(c =>
            //        c.FullName.Contains(searchTerm) ||
            //        c.PhoneNumber.Contains(searchTerm) ||
            //        c.Email.Contains(searchTerm));
            //}

            //if (sortOrder?.ToLower() == "desc")
            //{
            //    materialsQuery = materialsQuery.OrderByDescending(GetSortProperty(sortColumn));
            //}
            //else
            //{
            //    materialsQuery = materialsQuery.OrderBy(GetSortProperty(sortColumn));
            //}

            var materials = await PaginatedList<Material>.CreateAsync(materialsQuery, page, pageSize);

            return materials;
        }
    }
}
