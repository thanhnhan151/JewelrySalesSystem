using AutoMapper;
using FluentValidation;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Gems;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class GemService : IGemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateGemRequest> _createValidator;
        private readonly IValidator<UpdateGemRequest> _updateValidator;

        public GemService(
            IUnitOfWork unitOfWork
            , IMapper mapper, IValidator<CreateGemRequest> createValidator, IValidator<UpdateGemRequest> updateValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<PaginatedList<GetGemResponse>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        {
            var result = _mapper.Map<PaginatedList<GetGemResponse>>(await _unitOfWork.Gems.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize));

            foreach (var item in result.Items)
            {
                float price = 0;

                var gem = await _unitOfWork.Gems.GetEntityByIdAsync(item.GemId);

                if (gem != null)
                {
                    price = await _unitOfWork.Gems.GetGemPriceAsync(gem);

                    float shapePriceRate = await _unitOfWork.Gems.GetShapePriceRateAsync(gem.ShapeId);

                    price = price * (1 + shapePriceRate / 100);
                }

                item.Price = price;
            }

            return result;
        }


        public async Task<CreateGemRequest> AddAsync(CreateGemRequest createGemRequest)
        {

            var validator = await _createValidator.ValidateAsync(createGemRequest);
            if (!validator.IsValid)
            {
                throw new ValidationException(validator.Errors);
            }

            var gem = new Gem
            {
                GemName = createGemRequest.GemName,
                FeaturedImage = createGemRequest.FeaturedImage,
                OriginId = createGemRequest.OriginId,
                CaratId = createGemRequest.CaratId,
                ColorId = createGemRequest.ColorId,
                ClarityId = createGemRequest.ClarityId,
                CutId = createGemRequest.CutId,
                ShapeId = createGemRequest.ShapeId
            };

            float price = await _unitOfWork.Gems.GetGemPriceAsync(gem);

            float shapePriceRate = await _unitOfWork.Gems.GetShapePriceRateAsync(gem.ShapeId);

            var product = new Product
            {
                ProductName = createGemRequest.GemName,
                FeaturedImage = createGemRequest.FeaturedImage,
                ProductPrice = price * (1 + shapePriceRate / 100),
                ProductTypeId = 14
            };

            var produtResult = _unitOfWork.Products.AddEntity(product);

            var gemResult = _unitOfWork.Gems.AddEntity(gem);

            await _unitOfWork.CompleteAsync();

            return createGemRequest;
        }

        public async Task UpdateAsync(UpdateGemRequest updateGemRequest)
        {

            var validator = await _updateValidator.ValidateAsync(updateGemRequest);
            if (!validator.IsValid)
            {
                throw new ValidationException(validator.Errors);
            }
            var gem = new Gem
            {
                GemId = updateGemRequest.GemId,
                GemName = updateGemRequest.GemName,
                FeaturedImage = updateGemRequest.FeaturedImage,
                OriginId = updateGemRequest.OriginId,
                CaratId = updateGemRequest.CaratId,
                ColorId = updateGemRequest.ColorId,
                ClarityId = updateGemRequest.ClarityId,
                CutId = updateGemRequest.CutId,
                ShapeId = updateGemRequest.ShapeId
            };
            await _unitOfWork.Gems.UpdateGem(gem);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetGemResponse?> GetByIdWithIncludeAsync(int id)
        {
            var result = _mapper.Map<GetGemResponse>(await _unitOfWork.Gems.GetByIdWithIncludeAsync(id));

            float price = 0;

            var gem = await _unitOfWork.Gems.GetEntityByIdAsync(id);

            if (gem != null)
            {
                price = await _unitOfWork.Gems.GetGemPriceAsync(gem);

                float shapePriceRate = await _unitOfWork.Gems.GetShapePriceRateAsync(gem.ShapeId);

                price = price * (1 + shapePriceRate / 100);
            }

            result.Price = price;

            return result;
        }

        public async Task<GetGemResponse?> GetGemById(int id) => _mapper.Map<GetGemResponse>(await _unitOfWork.Gems.GetEntityByIdAsync(id));

        public async Task<float> GetGemPriceAsync(GemPriceRequest gemPriceRequest)
        {
            var gemPrice = new GemPriceList
            {
                CaratId = gemPriceRequest.CaratId,
                ClarityId = gemPriceRequest.ClarityId,
                ColorId = gemPriceRequest.ColorId,
                CutId = gemPriceRequest.CutId,
                OriginId = gemPriceRequest.OriginId
            };

            return await _unitOfWork.Gems.GetGemPriceAsync(gemPrice);
        }
    }
}
