using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

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

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                materialsQuery = materialsQuery.Where(c =>
                    c.MaterialName.Contains(searchTerm));
            }

            if (sortOrder?.ToLower() == "desc")
            {
                materialsQuery = materialsQuery.OrderByDescending(GetSortProperty(sortColumn));
            }
            else
            {
                materialsQuery = materialsQuery.OrderBy(GetSortProperty(sortColumn));
            }

            var materials = await PaginatedList<Material>.CreateAsync(materialsQuery, page, pageSize);

            return materials;
        }

        private static Expression<Func<Material, object>> GetSortProperty(string? sortColumn)
        => sortColumn?.ToLower() switch
        {
            "name" => material => material.MaterialName,
            //"dob" => material => material.DoB,
            _ => material => material.MaterialId
        };
    }
}
