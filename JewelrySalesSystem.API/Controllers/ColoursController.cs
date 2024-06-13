using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/colours")]
    [ApiController]
    public class ColoursController : ControllerBase
    {
        private readonly ILogger<ColoursController> _logger;
        private readonly IColourService _colourService;

        public ColoursController(
            ILogger<ColoursController> logger,
            IColourService colourService)
        {
            _logger = logger;
            _colourService = colourService;
        }

        #region Get All Colours
        /// <summary>
        /// Get all colours in the system
        /// </summary>
        /// <returns>A list of all colours</returns>
        /// <response code="200">Return all colours in the system</response>
        /// <response code="400">If no colours are in the system</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _colourService.GetAllAsync();

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

        #region Get Colour By Id
        /// <summary>
        /// Get a colour based on Id in the system
        /// </summary>
        /// <param name="id">Id of the colour you want to get</param>
        /// <returns>A colour</returns>
        /// <response code="200">Return a colour in the system</response>
        /// <response code="400">If the colour is null</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _colourService.GetByIdAsync(id);

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
                ErrorMessage = $"Colour with {id} does not exist"
            });
        }
        #endregion
    }
}
