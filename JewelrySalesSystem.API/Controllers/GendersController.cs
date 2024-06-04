using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.DAL.Entities;
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
        [HttpPost]
        public async Task<IActionResult> AddGender(Gender gender)
        {
            try
            {
                await _genderService.AddGender(gender);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
