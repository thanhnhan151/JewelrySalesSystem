using JewelrySalesSystem.BAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/[controller]")]
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
    }
}
