using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Products;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(
            IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<GetProductResponse>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => _mapper.Map<PaginatedList<GetProductResponse>>(await _unitOfWork.Products.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize));

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

        public async Task<GetProductResponse?> GetByIdWithIncludeAsync(int id) => _mapper.Map<GetProductResponse>(await _unitOfWork.Products.GetByIdWithIncludeAsync(id));
    }
}
