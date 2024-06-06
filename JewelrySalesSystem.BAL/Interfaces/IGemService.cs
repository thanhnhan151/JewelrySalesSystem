using JewelrySalesSystem.BAL.Models.Gems;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IGemService
    {
        Task<PaginatedList<GetGemResponse>> PaginationAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);

        Task<Gem> AddAsync(Gem gem);

        Task UpdateAsync(Gem gem);

        Task<GetGemResponse?> GetByIdWithIncludeAsync(int id);
    }
}
