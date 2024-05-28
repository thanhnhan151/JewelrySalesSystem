using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/warranties")]
    [ApiController]
    public class WarrantiesController : ControllerBase
    {
        private readonly ILogger<WarrantiesController> _logger;
        private readonly IWarrantyService _warrantyService;

        public WarrantiesController(
            ILogger<WarrantiesController> logger,
            IWarrantyService warrantyService)
        {
            _logger = logger;
            _warrantyService = warrantyService;
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
                var result = await _warrantyService.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

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
    }
}
