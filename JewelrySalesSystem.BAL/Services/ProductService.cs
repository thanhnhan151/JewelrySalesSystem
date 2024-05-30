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

        public async Task<Product> AddAsync(Product product)
        {
            var result = _unitOfWork.Products.AddEntity(product);

            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task UpdateAsync(Product product)
        {
            _unitOfWork.Products.UpdateEntity(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<Product?> GetByIdAsync(int id) => await _unitOfWork.Products.GetByIdAsync(id);
    }
}
