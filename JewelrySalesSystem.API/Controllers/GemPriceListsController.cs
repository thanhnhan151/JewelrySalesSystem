using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.GemPriceLists;
using Microsoft.AspNetCore.Mvc;


namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/gemPrices")]
    [ApiController]
    public class GemPriceListsController : ControllerBase
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IGemPriceListService _gemPriceListService;
        public GemPriceListsController(ILogger<RolesController> logger, IGemPriceListService gemPriceListService)
        {
            _logger = logger;
            _gemPriceListService = gemPriceListService;
        }

        [HttpPost]
        public async Task<IActionResult> AddGemPrice(CreateGemPriceRequest createGemPriceRequest)
        {
            var newGemPrice = await _gemPriceListService.AddGemPrice(createGemPriceRequest);
            return Ok(newGemPrice);
        }
    }
}
