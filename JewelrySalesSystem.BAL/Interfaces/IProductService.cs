using JewelrySalesSystem.BAL.Models.Products;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IProductService
    {
        Task<PaginatedList<GetProductResponse>> PaginationAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);

        Task<CreateProductRequest> AddAsync(CreateProductRequest createProductRequest);

        Task UpdateAsync(Product product);

        Task<GetProductResponse?> GetByIdWithIncludeAsync(int id);
    }
}
