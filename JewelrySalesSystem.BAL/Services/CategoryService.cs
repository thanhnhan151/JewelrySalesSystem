using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Categories;
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

        public async Task<List<GetRawCategoryResponse>> GetAllAsync() => _mapper.Map<List<GetRawCategoryResponse>>(await _unitOfWork.Categories.GetAllAsync());

        public async Task<GetCategoryResponse?> GetAllProductsByCategoryIdAsync(int id)
        {
            var result = _mapper.Map<GetCategoryResponse>(await _unitOfWork.Categories.GetAllProductsByCategoryIdAsync(id));

            if (result == null) return null;

            return result;
        }

        public async Task UpdateAsync(UpdateCategoryRequest updateCategoryRequest)
        {
            _unitOfWork.Categories.UpdateEntity(_mapper.Map<Category>(updateCategoryRequest));
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetRawCategoryResponse?> GetByIdAsync(int id) => _mapper.Map<GetRawCategoryResponse>(await _unitOfWork.Categories.GetEntityByIdAsync(id));

        public async Task<CreateCategoryRequest> AddNewCategory(CreateCategoryRequest createCategoryRequest)
        {
            var result = _unitOfWork.Categories.AddEntity(_mapper.Map<Category>(createCategoryRequest));

            await _unitOfWork.CompleteAsync();

            return createCategoryRequest;
        }
    }
}
