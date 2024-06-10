using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.GemPriceLists;
using Microsoft.AspNetCore.Mvc;


namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/gemprices")]
    [ApiController]
    public class GemPricesController : ControllerBase
    {
        private readonly ILogger<GemPricesController> _logger;
        private readonly IGemPriceListService _gemPriceListService;
        public GemPricesController(ILogger<GemPricesController> logger, IGemPriceListService gemPriceListService)
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
