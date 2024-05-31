using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/materials")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly ILogger<MaterialsController> _logger;
        private readonly IMaterialService _materialService;

        public MaterialsController(
            ILogger<MaterialsController> logger,
            IMaterialService materialService)
        {
            _logger = logger;
            _materialService = materialService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        {
            try
            {
                var result = await _materialService.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

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
        public async Task<IActionResult> AddAsync(Material material)
        {
            try
            {
                await _materialService.AddAsync(material);

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
                var result = await _materialService.GetByIdAsync(id);

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

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Material material)
        {
            try
            {
                await _materialService.UpdateAsync(material);

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
