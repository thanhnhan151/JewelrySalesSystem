using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IWarrantyService
    {
        Task<PaginatedList<Warranty>> PaginationAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);
    }
}
