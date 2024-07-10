using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.VnPays;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;

        public PaymentsController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentRequest request)
        {
            var url = _vnPayService.CreateUrl(request);

            return Ok(url);
        }
    }
}
