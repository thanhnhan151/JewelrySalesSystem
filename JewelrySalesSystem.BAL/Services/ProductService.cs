using AutoMapper;
using FluentValidation;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Gems;
using JewelrySalesSystem.BAL.Models.Materials;
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
        //changes here
        private readonly IValidator<CreateProductRequest> _addProductValidator;
        private readonly IValidator<UpdateProductRequest> _updateProductValidator;

        public ProductService(
            IUnitOfWork unitOfWork
            , IMapper mapper, IValidator<CreateProductRequest> validator, IValidator<UpdateProductRequest> updateProductValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _addProductValidator = validator;
            _updateProductValidator = updateProductValidator;
        }

        public async Task<PaginatedList<GetProductResponse>> ProductPaginationAsync(
            int productTypeId,
            int? categoryId,
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        {
            var result = _mapper.Map<PaginatedList<GetProductResponse>>(await _unitOfWork.Products.JewelryPaginationAsync(productTypeId, categoryId, searchTerm, sortColumn, sortOrder, page, pageSize));

            foreach (var item in result.Items)
            {
                foreach (var gem in item.Gems)
                {
                    gem.GemPrice.Total = CalculateTotal(gem);
                    RefactorGemPrice(gem);
                }

                CalculateProductPrice(item);
            }

            return result;
        }

        public async Task<PaginatedList<GetGemProductResponse>> GemPaginationAsync(
            int productTypeId,
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        {
            var result = _mapper.Map<PaginatedList<GetGemProductResponse>>(await _unitOfWork.Products.PaginationAsync(productTypeId, searchTerm, sortColumn, sortOrder, page, pageSize));

            foreach (var item in result.Items)
            {
                var gem = await _unitOfWork.Gems.GetByNameWithIncludeAsync(item.ProductName);

                if (gem != null)
                {
                    item.Origin = gem.Origin;
                    item.CaratWeight = gem.CaratWeight;
                    item.Cut = gem.Cut;
                    item.Colour = gem.Colour;
                    item.Clarity = gem.Clarity;
                    item.GemPrice = _mapper.Map<GemPrice>(gem.GemPrice);

                    item.GemPrice.Total = item.GemPrice.CaratWeightPrice * (1 + item.GemPrice.ColourPrice / 100 + item.GemPrice.CutPrice / 100 + item.GemPrice.ClarityPrice / 100);

                    item.GemPrice.ClarityPrice = item.GemPrice.ClarityPrice / 100;
                    item.GemPrice.ColourPrice = item.GemPrice.ColourPrice / 100;
                    item.GemPrice.CutPrice = item.GemPrice.CutPrice / 100;
                }
            }
            return result;
        }

        public async Task<PaginatedList<GetMaterialProductResponse>> MaterialPaginationAsync(
            int productTypeId,
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        {
            var result = _mapper.Map<PaginatedList<GetMaterialProductResponse>>(await _unitOfWork.Products.PaginationAsync(productTypeId, searchTerm, sortColumn, sortOrder, page, pageSize));

            foreach (var item in result.Items)
            {
                var material = await _unitOfWork.Materials.GetByNameWithIncludeAsync(item.ProductName);

                if (material != null)
                {
                    item.MaterialPrice = _mapper.Map<MaterialPrice>(material.MaterialPrices.SingleOrDefault());
                }
            }
            return result;
        }

        public async Task<CreateProductRequest> AddAsync(CreateProductRequest createProductRequest)
        {
            //change here
            var validation = await _addProductValidator.ValidateAsync(createProductRequest);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

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
                Weight = createProductRequest.Weight,
                ProductGems = productGems,
                ProductMaterials = productMaterials
            };

            var result = _unitOfWork.Products.AddEntity(product);

            await _unitOfWork.CompleteAsync();

            return createProductRequest;
        }

        public async Task UpdateAsync(UpdateProductRequest updateProductRequest)
        {
            //changes here
            var validation = await _updateProductValidator.ValidateAsync(updateProductRequest);
            if(!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            var productGems = new List<ProductGem>();
            if (updateProductRequest.Gems.Count > 0)
            {
                foreach (var item in updateProductRequest.Gems)
                {
                    productGems.Add(new ProductGem
                    {
                        GemId = item
                    });
                }
            }

            var productMaterials = new List<ProductMaterial>();
            if (updateProductRequest.Materials.Count > 0)
            {
                foreach (var item in updateProductRequest.Materials)
                {
                    productMaterials.Add(new ProductMaterial
                    {
                        MaterialId = item
                    });
                }
            }

            var product = new Product()
            {
                ProductId = updateProductRequest.ProductId,
                ProductName = updateProductRequest.ProductName,
                PercentPriceRate = updateProductRequest.PercentPriceRate,
                ProductionCost = updateProductRequest.ProductionCost,
                FeaturedImage = updateProductRequest.FeaturedImage,
                CategoryId = updateProductRequest.CategoryId,
                ProductTypeId = updateProductRequest.ProductTypeId,
                GenderId = updateProductRequest.GenderId,
                ColourId = updateProductRequest.ColourId,
                Weight = updateProductRequest.Weight,
                ProductGems = productGems,
                ProductMaterials = productMaterials
            };


            await _unitOfWork.Products.UpdateProduct(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetProductResponse?> GetByIdAsync(int id) => _mapper.Map<GetProductResponse>(await _unitOfWork.Products.GetByIdWithIncludeAsync(id));

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Products.DeleteProduct(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetProductResponse?> GetByIdWithIncludeAsync(int id)
        {
            var result = _mapper.Map<GetProductResponse>(await _unitOfWork.Products.GetByIdWithIncludeAsync(id));

            foreach (var item in result.Gems)
            {
                item.GemPrice.Total = CalculateTotal(item);
                RefactorGemPrice(item);
            }

            CalculateProductPrice(result);

            return result;
        }

        private static float CalculateTotal(GemItem gemItem)
            => gemItem.GemPrice.CaratWeightPrice * (1 + gemItem.GemPrice.ColourPrice / 100 + gemItem.GemPrice.CutPrice / 100 + gemItem.GemPrice.ClarityPrice / 100);

        private static void RefactorGemPrice(GemItem gem)
        {
            gem.GemPrice.ClarityPrice = gem.GemPrice.ClarityPrice / 100;
            gem.GemPrice.ColourPrice = gem.GemPrice.ColourPrice / 100;
            gem.GemPrice.CutPrice = gem.GemPrice.CutPrice / 100;
        }

        private void CalculateProductPrice(GetProductResponse productResponse)
        {
            productResponse.ProductPrice += productResponse.ProductionCost;

            if (productResponse.Gems.Count > 0)
            {
                foreach (var gem in productResponse.Gems)
                {
                    productResponse.ProductPrice += gem.GemPrice.Total;
                }
            }

            if (productResponse.Materials.Count > 0)
            {
                foreach (var material in productResponse.Materials)
                {
                    productResponse.ProductPrice += (productResponse.Weight * material.MaterialPrice.SellPrice);
                }
            }
            productResponse.ProductPrice += (productResponse.ProductPrice * (productResponse.PercentPriceRate) / 100);
        }
    }
}
