using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MaterialService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<Material>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => await _unitOfWork.Materials.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

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

        public async Task<Material?> GetByIdAsync(int id) => await _unitOfWork.Materials.GetByIdAsync(id);
    }
}
