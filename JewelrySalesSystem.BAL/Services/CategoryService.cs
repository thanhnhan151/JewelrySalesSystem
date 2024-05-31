using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<Category>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => await _unitOfWork.Categories.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

        public async Task<Category> AddAsync(Category category)
        {
            var result = _unitOfWork.Categories.AddEntity(category);

            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task UpdateAsync(Category category)
        {
            _unitOfWork.Categories.UpdateEntity(category);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<Category?> GetByIdAsync(int id) => await _unitOfWork.Categories.GetByIdAsync(id);
    }
}
