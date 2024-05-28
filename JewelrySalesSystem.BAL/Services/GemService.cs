using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class GemService : IGemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<Gem>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => await _unitOfWork.Gems.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);
    }
}
