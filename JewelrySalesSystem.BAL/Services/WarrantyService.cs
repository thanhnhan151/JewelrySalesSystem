﻿using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Roles;
using JewelrySalesSystem.BAL.Models.Users;
using JewelrySalesSystem.BAL.Models.Warranties;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using System.Data;

namespace JewelrySalesSystem.BAL.Services
{
    public class WarrantyService : IWarrantyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WarrantyService(IUnitOfWork unitOfWork,
                IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GetWarrantyResponse>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => _mapper.Map<PaginatedList<GetWarrantyResponse>>(await _unitOfWork.Warranties.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize));


        //Update information of warranty in the database
        //Use AutoMapper
        public async Task UpdateAsync(UpdateWarrantyRequest updateWarrantyRequest)
        {
            _unitOfWork.Warranties.UpdateEntity(_mapper.Map<Warranty>(updateWarrantyRequest));
            await _unitOfWork.CompleteAsync();
        }
        public async Task<GetWarrantyResponse> GetWarrantyById(int id)
        {
            var warranty = await _unitOfWork.Warranties.GetWarrantyById(id);

            if (warranty == null)
            {
                return null;
            }

            var mappedWarranty = _mapper.Map<GetWarrantyResponse>(warranty);
            return mappedWarranty;

        }
    }
}
