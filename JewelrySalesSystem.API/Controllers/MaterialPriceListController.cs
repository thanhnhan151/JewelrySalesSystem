using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.MaterialPriceList;
using JewelrySalesSystem.DAL.Infrastructures;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/materialPriceList")]
    [ApiController]
    public class MaterialPriceListController : ControllerBase
    {
        private readonly ILogger<MaterialPriceListController> _logger;
        private readonly IMaterialPriceListService _materialService;

        public MaterialPriceListController(ILogger<MaterialPriceListController> logger, IMaterialPriceListService materialService)
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
