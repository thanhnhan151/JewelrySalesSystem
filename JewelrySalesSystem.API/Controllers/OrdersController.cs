using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderService _orderService;

        public OrdersController(
            ILogger<OrdersController> logger,
            IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        #region Get All Orders
        /// <summary>
        /// Get all orders in the system
        /// </summary>
        /// <returns>A list of all orders</returns>
        /// <response code="200">Return all orders in the system</response>
        /// <response code="400">If no orders are in the system</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _orderService.GetAllAsync();

                if (result is not null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return NotFound();
        }
        #endregion

        #region Get Order By Id
        /// <summary>
        /// Get a order based on Id in the system
        /// </summary>
        /// <param name="id">Id of the order you want to get</param>
        /// <returns>A order</returns>
        /// <response code="200">Return a order in the system</response>
        /// <response code="400">If the order is null</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _orderService.GetByIdWithIncludeAsync(id);

                if (result is not null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return NotFound(new
            {
                ErrorMessage = $"Order with {id} does not exist"
            });
        }
        #endregion

        #region Add Order
        /// <summary>
        /// Add an order in the system
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///       "customerName": "Test Customer",
        ///       "userName": "Test Creator",
        ///       "warranty": "Test Warranty",
        ///       "total": 6000000,
        ///       "orderDetails": [
        ///         {
        ///           "productName": "Test Product 1",
        ///           "sellPrice": 2000000,
        ///           "buyPrice": 1500000,
        ///           "perDiscount": 10
        ///         },
        ///         {
        ///           "productName": "Test Product 2",
        ///           "sellPrice": 1000000,
        ///           "buyPrice": 950000,
        ///           "perDiscount": 5
        ///         }
        ///       ]
        ///     }
        ///         
        /// </remarks> 
        /// <returns>Order that was created</returns>
        /// <response code="200">Invoice that was created</response>
        /// <response code="400">Failed validation</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateUpdateOrderRequest createRequest)
        {
            try
            {
                await _orderService.AddAsync(createRequest);

                return Ok(createRequest);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Update Order
        #endregion
    }
}
