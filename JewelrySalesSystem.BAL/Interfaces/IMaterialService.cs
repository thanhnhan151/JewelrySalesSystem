using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IMaterialService
    {
        Task<PaginatedList<Material>> PaginationAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);

        Task<Material> AddAsync(Material material);

        Task UpdateAsync(Material material);

        Task<Material?> GetByIdAsync(int id);
    }
}
