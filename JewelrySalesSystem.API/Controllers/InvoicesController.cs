using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.DAL.Entities;
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

        public InvoicesController(
            ILogger<InvoicesController> logger,
            IInvoiceService invoiceService)
        {
            _logger = logger;
            _invoiceService = invoiceService;
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
        ///       "userName": "testaccount",
        ///       "fullName": "Nguyen Van B",
        ///       "phoneNumber": "0999123456",
        ///       "email": "testemail@gmail.com",
        ///       "password" : "test",
        ///       "address" : "test",
        ///       "roleId" : 3
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
        public async Task<IActionResult> AddAsync(Invoice invoice)
        {
            try
            {
                await _invoiceService.AddAsync(invoice);

                return Ok();
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

            return BadRequest();
        }
        #endregion

        #region Update Invoice
        /// <summary>
        /// Update an invoice in the system
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///       "userId" : 2,
        ///       "userName": "newtestaccount",
        ///       "fullName": "Nguyen Van C",
        ///       "phoneNumber": "0999123456",
        ///       "email": "testemail@gmail.com",
        ///       "password" : "test",
        ///       "address" : "test",
        ///       "roleId" : 2
        ///     }
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
        public async Task<IActionResult> UpdateAsync(Invoice invoice)
        {
            try
            {
                await _invoiceService.UpdateAsync(invoice);

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
