using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.DAL.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PaginatedList<Product>> JewelryPaginationAsync
            (int productTypeId
            , int? categoryId
            , string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);

        Task<PaginatedList<Product>> PaginationAsync
            (int productTypeId
            , string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);

        Task<Product?> GetByIdWithIncludeAsync(int id);

        Task UpdateProduct(Product product);

        Task DeleteProduct(int id);

        Task<float> GetWeightAsync(int productId, int materialId);

        //changes here
        Task<Product> CheckDuplicate(string productName);

        Task<bool> CategoryExit(int categoryId);

        Task<bool> ProductTypeExit(int productTypeId);

        Task<bool> GenderExit(int genderId);
    }
}
