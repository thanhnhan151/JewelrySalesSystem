using JewelrySalesSystem.BAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/[controller]")]
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
    }
}
