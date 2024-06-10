using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.MaterialPriceList;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/materialprices")]
    [ApiController]
    public class MaterialPricesController : ControllerBase
    {
        private readonly ILogger<MaterialPricesController> _logger;
        private readonly IMaterialPriceListService _materialService;

        public MaterialPricesController(ILogger<MaterialPricesController> logger, IMaterialPriceListService materialService)
        {
            _logger = logger;
            _materialService = materialService;
        }

        #region
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateMaterialPriceList createMaterialPriceList)
        {
            try
            {
                var result = await _materialService.AddAsync(createMaterialPriceList);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion
    }
}
