using JewelrySalesSystem.BAL.Models.Categories;
using JewelrySalesSystem.BAL.Models.Products;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface ICategoryService
    {
        Task<List<GetCategoryResponse>> GetAllAsync();

        Task<GetCategoryResponse?> GetAllProductsByCategoryIdAsync(int id);

        Task UpdateAsync(Category category);

        Task<Category?> GetByIdAsync(int id);

        Task<AddCategories> AddNewCategory(AddCategories category);
    }
}
