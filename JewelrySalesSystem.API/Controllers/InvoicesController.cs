using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.BAL.Validators;
using JewelrySalesSystem.BAL.Validators.Invoices;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/invoices")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly ILogger<InvoicesController> _logger;
        private readonly IInvoiceService _invoiceService;
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;
        private readonly IWarrantyService _warrantyService;
        private readonly IProductService _productService;


        public InvoicesController(
            ILogger<InvoicesController> logger,
            IInvoiceService invoiceService, ICustomerService customerService, IUserService userService, IWarrantyService warrantyService, IProductService productService)
        {
            _logger = logger;
            _invoiceService = invoiceService;
            _customerService = customerService;
            _userService = userService;
            _productService = productService;
            _warrantyService = warrantyService;
        }

        #region Get All Invoices
        /// <summary>
        /// Get all invoices in the system
        /// </summary>
        /// <param name="page">Current page the user is on</param>
        /// <param name="pageSize">Number of entities you want to show</param>
        /// <param name="searchTerm">Search query</param>
        /// <param name="sortColumn">Column you want to sort</param>
        /// <param name="sortOrder">Sort column by ascending or descening</param>
        /// <returns>A list of all users</returns>
        /// <response code="200">Return all invoices in the system</response>
        /// <response code="400">If no invoices are in the system</response>
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
                var result = await _invoiceService.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

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

        #region Add Invoice
        /// <summary>
        /// Add an invoice in the system
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///       "status": true,
        ///       "invoiceType": true,
        ///       "customerName": "Tran Van A",
        ///       "userId": 1,
        ///       "warrantyId": 1,
        ///       "invoiceDetails": [
        ///         1,
        ///         2
        ///       ]
        ///     }
        ///         
        /// </remarks> 
        /// <returns>Invoice that was created</returns>
        /// <response code="200">Invoice that was created</response>
        /// <response code="400">Failed validation</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateInvoiceRequest createInvoiceRequest)
        {
           
            try
            {
                //Use Fluent Validation
                var validator = new CreateInvoiceRequestValidator(_customerService, _userService, _warrantyService,_productService);
                var result = await validator.ValidateAsync(createInvoiceRequest);
                if (!result.IsValid)
                {
                    //Add all error messages to an array
                    var errorMessages = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        errorMessages.Add(error.ErrorMessage);
                    }
                    return BadRequest(errorMessages);
                }

                await _invoiceService.AddAsync(createInvoiceRequest);

                return Ok(createInvoiceRequest);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Get Invoice By Id
        /// <summary>
        /// Get an invoice in the system
        /// </summary>
        /// <param name="id">Id of the invoice you want to get</param>
        /// <returns>An user</returns>
        /// <response code="200">Return an invoice in the system</response>
        /// <response code="400">If the invoice is null</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _invoiceService.GetByIdWithIncludeAsync(id);

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
                ErrorMessage = "Invoice does not exist"
            });
        }
        #endregion

        #region Update Invoice
        /// <summary>
        /// Update an invoice in the system
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///             {
        ///             "invoiceId": 0,
        ///             "status": true,
        ///             "invoiceType": true,
        ///             "customerName": "Tran Van A",
        ///             "userId": 0,
        ///             "warrantyId": 0,
        ///             "invoiceDetails": [
        ///                    productId: 0
        ///                ]
        ///              }
        ///         
        /// 
        /// </remarks> 
        /// <returns>No content</returns>
        /// <response code="204">No content</response>
        /// <response code="400">Invoice does not exist</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateInvoiceRequest invoice)
        {
            try
            {
                //Use Fluent Validation
                var validator = new UpdateInvoiceRequestValidator(_customerService, _userService, _warrantyService, _productService, _invoiceService);
                var result = await validator.ValidateAsync(invoice);
                if (!result.IsValid)
                {
                    //Add all error messages to an array
                    var errorMessages = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        errorMessages.Add(error.ErrorMessage);
                    }
                    return BadRequest(errorMessages);
                }
                await _invoiceService.UpdateAsync(invoice);

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Delete Invoice
        /// <summary>
        /// Change the invoice status in the system
        /// </summary>
        /// <param name="id">Id of the invoice you want to change</param>
        /// <response code="204">No Content</response>
        /// <response code="400">If the invoice is null</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            try
            {
                var result = await _invoiceService.GetByIdAsync(id);

                if (result == null) return NotFound(new
                {
                    ErrorMessage = $"Invoice with {id} does not exist"
                });

                await _invoiceService.DeleteInvoice(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
