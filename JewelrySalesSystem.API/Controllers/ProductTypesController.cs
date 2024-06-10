using JewelrySalesSystem.BAL.Interfaces;
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
    }
}
