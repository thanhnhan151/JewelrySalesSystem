﻿using AutoMapper;
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

        public MaterialService(
            IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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

        public async Task UpdateAsync(Material material)
        {
            _unitOfWork.Materials.UpdateEntity(material);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetMaterialResponse?> GetByIdWithIncludeAsync(int id) => _mapper.Map<GetMaterialResponse>(await _unitOfWork.Materials.GetByIdWithIncludeAsync(id));

        public async Task<CreateMaterialRequest> AddAsync(CreateMaterialRequest createMaterialRequest)
        {
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
    }
}
