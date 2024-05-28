using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<Product>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => await _unitOfWork.Products.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);
    }
}
