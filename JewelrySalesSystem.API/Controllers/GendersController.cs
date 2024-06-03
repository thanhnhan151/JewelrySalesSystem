using JewelrySalesSystem.BAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/genders")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly ILogger<GendersController> _logger;
        private readonly IGenderService _genderService;

        public GendersController(
            ILogger<GendersController> logger,
            IGenderService genderService)
        {
            _logger = logger;
            _genderService = genderService;
        }
    }
}
