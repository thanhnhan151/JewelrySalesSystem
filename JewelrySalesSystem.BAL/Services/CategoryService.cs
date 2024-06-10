using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Categories;
using JewelrySalesSystem.BAL.Models.Products;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetCategoryResponse>> GetAllAsync() => _mapper.Map<List<GetCategoryResponse>>(await _unitOfWork.Categories.GetAllAsync());

        public async Task<GetCategoryResponse?> GetAllProductsByCategoryIdAsync(int id)
        {
            var result = _mapper.Map<GetCategoryResponse>(await _unitOfWork.Categories.GetAllProductsByCategoryIdAsync(id));

            if (result == null) return null;

            return result;
        }

        public async Task UpdateAsync(Category category)
        {
            _unitOfWork.Categories.UpdateEntity(category);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<Category?> GetByIdAsync(int id) => await _unitOfWork.Categories.GetByIdAsync(id);

        public async Task<Category> AddNewCategory(Category category)
        {
            var result = _unitOfWork.Categories.AddEntity(category);

            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task<AddCategories> AddNewCategory(AddCategories category)
        {
            var result = _unitOfWork.Categories.AddEntity(_mapper.Map<Category>(category));

            await _unitOfWork.CompleteAsync();

            var newCategories = _mapper.Map<AddCategories>(result);

            return newCategories;
        }
    }
}
