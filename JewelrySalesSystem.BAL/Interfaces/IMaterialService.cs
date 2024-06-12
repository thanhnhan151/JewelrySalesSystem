using JewelrySalesSystem.BAL.Models.Materials;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IMaterialService
    {
        Task<PaginatedList<GetMaterialResponse>> PaginationAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);

        Task<List<GetMaterialResponse>> GetAllGoldMaterials();

        Task<CreateMaterialRequest> AddAsync(CreateMaterialRequest createMaterialRequest);

        Task<GetMaterialResponse?> GetByIdAsync(int id);

        Task UpdateAsync(Material material);

        Task<GetMaterialResponse?> GetByIdWithIncludeAsync(int id);
    }
}
