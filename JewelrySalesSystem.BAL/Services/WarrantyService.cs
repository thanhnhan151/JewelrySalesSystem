using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class WarrantyService : IWarrantyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WarrantyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<Warranty>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => await _unitOfWork.Warranties.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);
    }
}
