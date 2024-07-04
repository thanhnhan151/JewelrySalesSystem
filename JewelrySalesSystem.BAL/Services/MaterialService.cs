using AutoMapper;
using FluentValidation;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Materials;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //changes here
        private readonly IValidator<CreateMaterialRequest> _createValidator;

        public MaterialService(
            IUnitOfWork unitOfWork
            , IMapper mapper, IValidator<CreateMaterialRequest> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _createValidator = validator;
        }

        public async Task<PaginatedList<GetMaterialResponse>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => _mapper.Map<PaginatedList<GetMaterialResponse>>(await _unitOfWork.Materials.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize));

        public async Task<Material> AddAsync(Material material)
        {
            var result = _unitOfWork.Materials.AddEntity(material);

            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task UpdateAsync(UpdateMaterialRequest updateMaterialRequest)
        {
            var material = await _unitOfWork.Materials.GetEntityByIdAsync(updateMaterialRequest.MaterialId);

            if (material != null)
            {
                material.MaterialName = updateMaterialRequest.MaterialName;
                _unitOfWork.Materials.UpdateEntity(material);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<GetMaterialResponse?> GetByIdWithIncludeAsync(int id) => _mapper.Map<GetMaterialResponse>(await _unitOfWork.Materials.GetByIdWithIncludeAsync(id));

        public async Task<CreateMaterialRequest> AddAsync(CreateMaterialRequest createMaterialRequest)
        {
            //changes here
            var validation = await _createValidator.ValidateAsync(createMaterialRequest);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            var material = new Material
            {
                MaterialName = createMaterialRequest.MaterialName,
                MaterialPrices = new List<MaterialPriceList>
                {
                    new MaterialPriceList
                    {
                        SellPrice = createMaterialRequest.MaterialPrice.SellPrice,
                        BuyPrice = createMaterialRequest.MaterialPrice.BuyPrice,
                        EffDate = DateTime.Now
                    }
                }
            };

            var product = new Product
            {
                ProductName = createMaterialRequest.MaterialName,
                ProductPrice = createMaterialRequest.MaterialPrice.SellPrice,
                ProductTypeId = 2
            };

            var produtResult = _unitOfWork.Products.AddEntity(product);

            var materialResult = _unitOfWork.Materials.AddEntity(material);

            await _unitOfWork.CompleteAsync();

            return createMaterialRequest;
        }

        public async Task<GetMaterialResponse?> GetByIdAsync(int id) => _mapper.Map<GetMaterialResponse>(await _unitOfWork.Materials.GetEntityByIdAsync(id));

        public async Task<List<GetMaterialResponse>> GetAllGoldMaterials() => _mapper.Map<List<GetMaterialResponse>>(await _unitOfWork.Materials.GetAllGoldMaterials());

        public async Task DeleteAsync(int id)
        {
            var result = await _unitOfWork.Materials.GetEntityByIdAsync(id);

            if (result != null)
            {
                result.IsActive = false;
                _unitOfWork.Materials.UpdateEntity(result);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
