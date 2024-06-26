using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.DAL.Interfaces
{
    public interface IGemRepository : IGenericRepository<Gem>
    {
        Task<PaginatedList<Gem>> PaginationAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);

        Task<Gem?> GetByNameWithIncludeAsync(string name);

        Task<Gem?> GetByIdWithIncludeAsync(int id);

        Task UpdateGem(Gem gem);
    }
}
