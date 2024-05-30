using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface ICategoryService
    {
        Task<PaginatedList<Category>> PaginationAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);

        Task<Category> AddAsync(Category category);

        Task UpdateAsync(Category category);

        Task<Category?> GetByIdAsync(int id);
    }
}
