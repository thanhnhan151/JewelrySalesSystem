using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.DAL.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PaginatedList<Product>> PaginationAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);

        Task<Product?> GetByIdWithIncludeAsync(int id);

        Task UpdateProduct(Product product);

        Task DeleteProduct(int id);
    }
}
