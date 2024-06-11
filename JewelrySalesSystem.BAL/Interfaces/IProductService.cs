using JewelrySalesSystem.BAL.Models.Products;
using JewelrySalesSystem.DAL.Common;

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

        Task UpdateAsync(UpdateProductRequest updateProductRequest);

        Task<GetProductResponse?> GetByIdAsync(int id);

        Task<GetProductResponse?> GetByIdWithIncludeAsync(int id);

        Task DeleteAsync(int id);
    }
}
