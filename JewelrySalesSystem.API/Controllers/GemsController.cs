using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/gems")]
    [ApiController]
    public class GemsController : ControllerBase
    {
        private readonly ILogger<GemsController> _logger;
        private readonly IGemService _gemService;

        public GemsController(
            ILogger<GemsController> logger,
            IGemService gemService)
        {
            _logger = logger;
            _gemService = gemService;
        }

        #region Get All Gems
        /// <summary>
        /// Get all gems in the system
        /// </summary>
        /// <param name="page">Current page the user is on</param>
        /// <param name="pageSize">Number of entities you want to show</param>
        /// <param name="searchTerm">Search query</param>
        /// <param name="sortColumn">Column you want to sort</param>
        /// <param name="sortOrder">Sort column by ascending or descening</param>
        /// <returns>A list of all users</returns>
        /// <response code="200">Return all gems in the system</response>
        /// <response code="400">If no gems are in the system</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        {
            try
            {
                var result = await _gemService.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

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

        #region Add Gem
        /// <summary>
        /// Add a gem in the system
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
        /// <returns>Gem that was created</returns>
        /// <response code="200">Gem that was created</response>
        /// <response code="400">Failed validation</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        public async Task<IActionResult> AddAsync(Gem gem)
        {
            try
            {
                await _gemService.AddAsync(gem);

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Get Gem By Id
        /// <summary>
        /// Get a gem in the system
        /// </summary>
        /// <param name="id">Id of the gem you want to get</param>
        /// <returns>A product</returns>
        /// <response code="200">Return a gem in the system</response>
        /// <response code="400">If the gem is null</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _gemService.GetByIdAsync(id);

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

        #region Update Gem
        /// <summary>
        /// Update a gem in the system
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
        /// <response code="400">Gem does not exist</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Gem gem)
        {
            try
            {
                await _gemService.UpdateAsync(gem);

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
