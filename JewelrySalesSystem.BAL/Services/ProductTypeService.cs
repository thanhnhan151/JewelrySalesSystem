﻿using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.ProductTypes;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ProductTypeIdCollectionResponse?> GetAllProductsByProductTypeIdAsync(int productTypeId)
            => _mapper.Map<ProductTypeIdCollectionResponse>(await _unitOfWork.ProductTypes.GetAllProductsByProductTypeIdAsync(productTypeId));


        public async Task<List<GetProductTypeResponse>> GetAllAsync() => _mapper.Map<List<GetProductTypeResponse>>(await _unitOfWork.ProductTypes.GetAllAsync());

        public async Task<GetProductTypeResponse?> GetByIdAsync(int id) => _mapper.Map<GetProductTypeResponse>(await _unitOfWork.ProductTypes.GetEntityByIdAsync(id));
    }
}
