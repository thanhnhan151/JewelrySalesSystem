using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(
            JewelryDbContext context,
            ILogger logger) : base(context, logger)
        {
        }

        public async Task<List<Category>> GetAllAsync() => await _dbSet
                                                                 .Where(c => c.Status)
                                                                 .ToListAsync();

        public async Task<Category?> GetAllProductsByCategoryIdAsync(int id)
            => await _dbSet.Include(c => c.Products)
                                .ThenInclude(p => p.Gender)
                            .Include(c => c.Products)
                                .ThenInclude(p => p.Colour)
                            .Include(c => c.Products)
                                .ThenInclude(p => p.ProductType)
                           .FirstOrDefaultAsync(c => c.Status && c.CategoryId == id);

        public async Task<Category> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        //changes here
        public async void UpdateCategories(Category category)
        {
            var result = _dbSet.Find(category.CategoryId);
            var found = result;
            if(found is not null)
            {
                found.CategoryName = category.CategoryName;
                found.Status = category.Status;
                _context.Categories.Update(found);
            }
            else
            {
                throw new Exception($"Category with '{category.CategoryId}' not found!");
            }
        }

    }
}
