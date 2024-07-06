using JewelrySalesSystem.BAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(
            ILogger<CustomersController> logger,
            ICustomerService customerService)
        {
            _customerService = customerService;
            _logger = logger;
        }

        #region Get Customer Point By Phone Number
        /// <summary>
        /// Get customer discount based on name in the system
        /// </summary>
        /// <param name="phoneNumber">Customer phone number</param>
        /// <returns>Customer point</returns>
        /// <response code="200">Return Customer point</response>
        /// <response code="400">If the customer is null</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpGet("{phoneNumber}/discount")]
        public async Task<IActionResult> GetCustomerPointByNameAsync(string phoneNumber)
        {
            try
            {
                var result = await _customerService.GetCustomerByPhoneAsync(phoneNumber);

                if (result != null)
                {
                    return Ok(result.Point / 10);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return NotFound(new
            {
                ErrorMessage = $"Customer with  phone number: {phoneNumber} does not exist"
            });
        }
        #endregion
    }
}
