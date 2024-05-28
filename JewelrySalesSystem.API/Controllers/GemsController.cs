using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/gems")]
    [ApiController]
    public class GemsController : ControllerBase
    {
        private readonly ILogger<GemsController> _logger;
        private readonly IGemService _gemService;

        public GemsController(
            ILogger<GemsController> logger,
            IGemService gemService)
        {
            _logger = logger;
            _gemService = gemService;
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
                var result = await _gemService.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

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
