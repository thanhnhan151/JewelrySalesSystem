using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Users;
using JewelrySalesSystem.BAL.Models.Warranties;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

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

        public async Task<PaginatedList<Warranty>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => await _unitOfWork.Warranties.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

        //Update information of warranty in the database
        //Use AutoMapper
        public async Task UpdateAsync(UpdateWarrantyRequest updateWarrantyRequest)
        {
            _unitOfWork.Warranties.UpdateEntity(_mapper.Map<Warranty>(updateWarrantyRequest));
            await _unitOfWork.CompleteAsync();
        }
    }
}
