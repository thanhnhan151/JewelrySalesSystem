using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Genders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/genders")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly ILogger<GendersController> _logger;
        private readonly IGenderService _genderService;

        public GendersController(
            ILogger<GendersController> logger,
            IGenderService genderService)
        {
            _logger = logger;
            _genderService = genderService;
        }

        #region Add Gender
        /// <summary>
        /// Add a gender in the system
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///       "genderName": "Test Gender"
        ///     }
        ///         
        /// </remarks> 
        /// <returns>Gender that was created</returns>
        /// <response code="200">Gender that was created</response>
        /// <response code="400">Failed validation</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        public async Task<IActionResult> AddGender(CreateGenderRequest createGenderRequest)
        {
            try
            {
                var result = await _genderService.AddGender(createGenderRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
