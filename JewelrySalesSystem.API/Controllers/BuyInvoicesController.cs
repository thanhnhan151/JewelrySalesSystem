﻿using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.BuyInvoices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/buyinvoices")]
    [ApiController]
    public class BuyInvoicesController : ControllerBase
    {
        private readonly ILogger<BuyInvoicesController> _logger;
        private readonly IBuyInvoiceService _buyInvoiceService;

        public BuyInvoicesController(
            ILogger<BuyInvoicesController> logger,
            IBuyInvoiceService buyInvoiceService)
        {
            _logger = logger;
            _buyInvoiceService = buyInvoiceService;
        }

        #region Get All Buy Invoices
        /// <summary>
        /// Get all buy invoices in the system
        /// </summary>
        /// <returns>A list of all buy invoices</returns>
        /// <response code="200">Return all buy invoices in the system</response>
        /// <response code="400">If no buy invoices are in the system</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _buyInvoiceService.GetAllAsync();

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

        #region Get Buy Invoice By Id
        /// <summary>
        /// Get a buy invoice based on Id in the system
        /// </summary>
        /// <param name="id">Id of the buy invoice you want to get</param>
        /// <returns>A buy invoice</returns>
        /// <response code="200">Return a buy invoice in the system</response>
        /// <response code="400">If the buy invoice is null</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _buyInvoiceService.GetByIdWithIncludeAsync(id);

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

        #region Add Buy Invoice
        /// <summary>
        /// Add a buy invoice in the system
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///       "customerName": "Test Buy Customer",
        ///       "userName": "Test Buy Creator",
        ///       "items": [
        ///         {
        ///           "productName": "Test Buy Product 1",
        ///           "purchaseTotal": 2000000,
        ///           "perDiscount": 10
        ///         },
        ///         {
        ///           "productName": "Test Buy Product 2",
        ///           "purchaseTotal": 1000000,
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
        public async Task<IActionResult> AddAsync([FromBody] CreateUpdateBuyInvoiceRequest createRequest)
        {
            try
            {
                await _buyInvoiceService.AddAsync(createRequest);

                return Ok(createRequest);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Update Invoice
        #endregion

        #region Update Order Status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeBuyInvoiceStatusAsync(int id)
        {
            try
            {
                var result = await _buyInvoiceService.GetByIdAsync(id);

                if (result != null)
                {
                    await _buyInvoiceService.ChangeBuyInvoiceStatusAsync(id);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return NotFound(new
            {
                ErrorMessage = $"Buy Invoice with {id} does not exist"
            });
        }
        #endregion

        #region Cancel Order
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelBuyInvoiceAsync(int id)
        {
            try
            {
                var result = await _buyInvoiceService.GetByIdAsync(id);

                if (result != null)
                {
                    await _buyInvoiceService.CancelBuyInvoiceAsync(id);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return NotFound(new
            {
                ErrorMessage = $"Buy Invoice with {id} does not exist"
            });
        }
        #endregion
    }
}