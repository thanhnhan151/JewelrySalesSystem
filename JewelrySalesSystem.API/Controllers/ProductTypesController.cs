using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/producttypes")]
    [ApiController]
    public class ProductTypesController : ControllerBase
    {
        private readonly ILogger<ProductTypesController> _logger;
        private readonly IProductTypeService _productTypeService;

        public ProductTypesController(
            ILogger<ProductTypesController> logger,
            IProductTypeService productTypeService)
        {
            _logger = logger;
            _productTypeService = productTypeService;
        }

        //changes here
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProductType()
        {
            try
            {
                var result = await _productTypeService.GetAllAsync();

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGetProductTypeById(int id)
        {
            try
            {
                var result = await _productTypeService.GetProductTypeByIdAsync(id);
                if(result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
