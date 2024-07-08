using JewelrySalesSystem.BAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/counters")]
    [ApiController]
    public class CountersController : ControllerBase
    {
        private readonly ILogger<CountersController> _logger;
        private readonly ICounterService _counterService;

        public CountersController(
            ILogger<CountersController> logger,
            ICounterService counterService)
        {
            _logger = logger;
            _counterService = counterService;
        }

        #region Get All Counters
        /// <summary>
        /// Get all counters in the system
        /// </summary>
        /// <returns>A list of all counters</returns>
        /// <response code="200">Return all counters in the system</response>
        /// <response code="400">If no counters are in the system</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _counterService.GetAllAsync();

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
    }
}
