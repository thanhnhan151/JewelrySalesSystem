using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.Extensions.Logging;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(
            JewelryDbContext context, 
            ILogger logger) : base(context, logger)
        {
        }

        public async Task<PaginatedList<Category>> PaginationAsync(
            string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize)
        {
            IQueryable<Category> categoriesQuery = _dbSet;

            //if (!string.IsNullOrWhiteSpace(searchTerm))
            //{
            //    categoriesQuery = categoriesQuery.Where(c =>
            //        c.FullName.Contains(searchTerm) ||
            //        c.PhoneNumber.Contains(searchTerm) ||
            //        c.Email.Contains(searchTerm));
            //}

            //if (sortOrder?.ToLower() == "desc")
            //{
            //    categoriesQuery = categoriesQuery.OrderByDescending(GetSortProperty(sortColumn));
            //}
            //else
            //{
            //    categoriesQuery = categoriesQuery.OrderBy(GetSortProperty(sortColumn));
            //}

            var categories = await PaginatedList<Category>.CreateAsync(categoriesQuery, page, pageSize);

            return categories;
        }
    }
}
