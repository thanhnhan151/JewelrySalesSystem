using JewelrySalesSystem.BAL.Interfaces;
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
    }
}
