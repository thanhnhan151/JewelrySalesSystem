using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IProductService
    {
        Task<PaginatedList<Product>> PaginationAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);
    }
}
