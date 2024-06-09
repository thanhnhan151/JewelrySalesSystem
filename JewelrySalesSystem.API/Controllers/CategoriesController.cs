using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Categories;
using JewelrySalesSystem.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(
            ILogger<CategoriesController> logger,
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page = 1,
            int pageSize = 5)
        {
            try
            {
                var result = await _categoryService.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

                if (result is not null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Category category)
        {
            try
            {
                await _categoryService.AddAsync(category);

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _categoryService.GetByIdAsync(id);

                if (result is not null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return NotFound(new
            {
                ErrorMessage = "Category does not exist"
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Category category)
        {
            try
            {
                await _categoryService.UpdateAsync(category);

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //changes here
        [HttpPost("create")]
        public async Task<IActionResult> CreateNewcategories(AddCategories category)
        {
            try
            {
                await _categoryService.AddNewCategory(category);

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
