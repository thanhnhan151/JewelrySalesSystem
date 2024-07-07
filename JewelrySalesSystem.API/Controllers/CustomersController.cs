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

        #region Get All Customers
        /// <summary>
        /// Get all customers in the system
        /// </summary>
        /// <param name="page">Current page the customer is on</param>
        /// <param name="pageSize">Number of entities you want to show</param>
        /// <param name="searchTerm">Search query</param>
        /// <param name="sortColumn">Column you want to sort</param>
        /// <param name="sortOrder">Sort column by ascending or descening</param>
        /// <returns>A list of all customers</returns>
        /// <response code="200">Return all customers in the system</response>
        /// <response code="400">If no customers are in the system</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page = 1,
            int pageSize = 5)
        {
            try
            {
                var result = await _customerService.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

                if (result is not null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return BadRequest();
        }
        #endregion


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
                    if (result.Point > 0 && result.Point < 99)
                    {
                        result.Discount = 5;
                    }
                    else if (result.Point > 99 && result.Point < 199)
                    {
                        result.Discount = 10;
                    }
                    else if (result.Point > 199)
                    {
                        result.Discount = 15;
                    }

                    return Ok(result.Discount);
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
