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

        public async Task<CreateProductRequest> AddAsync(CreateProductRequest createProductRequest)
        {
            var productGems = new List<ProductGem>();

            if (createProductRequest.Gems.Count > 0)
            {
                foreach (var item in createProductRequest.Gems)
                {
                    productGems.Add(new ProductGem
                    {
                        GemId = item
                    });
                }
            }

            var productMaterials = new List<ProductMaterial>();

            if (createProductRequest.Materials.Count > 0)
            {
                foreach (var item in createProductRequest.Materials)
                {
                    productMaterials.Add(new ProductMaterial
                    {
                        MaterialId = item
                    });
                }
            }

            var product = new Product()
            {
                ProductName = createProductRequest.ProductName,
                PercentPriceRate = createProductRequest.PercentPriceRate,
                ProductionCost = createProductRequest.ProductionCost,
                FeaturedImage = createProductRequest.FeaturedImage,
                CategoryId = createProductRequest.CategoryId,
                ProductTypeId = createProductRequest.ProductTypeId,
                GenderId = createProductRequest.GenderId,
                ColourId = createProductRequest.ColourId,
                ProductGems = productGems,
                ProductMaterials = productMaterials
            };

            var result = _unitOfWork.Products.AddEntity(product);

            await _unitOfWork.CompleteAsync();

            return createProductRequest;
        }

        public async Task UpdateAsync(UpdateProductRequest updateProductRequest)
        {
            _unitOfWork.Products.UpdateEntity(_mapper.Map<Product>(updateProductRequest));
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetProductResponse?> GetByIdWithIncludeAsync(int id) => _mapper.Map<GetProductResponse>(await _unitOfWork.Products.GetByIdWithIncludeAsync(id));

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Products.DeleteProduct(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
