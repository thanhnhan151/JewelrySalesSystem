using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.MaterialPriceList;
using JewelrySalesSystem.BAL.Models.Materials;
using JewelrySalesSystem.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/materials")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly ILogger<MaterialsController> _logger;
        private readonly IMaterialService _materialService;
        private readonly IMaterialPriceListService _materialPriceListService;

        public MaterialsController(
            ILogger<MaterialsController> logger,
            IMaterialService materialService,
            IMaterialPriceListService materialPriceListService)
        {
            _logger = logger;
            _materialService = materialService;
            _materialPriceListService = materialPriceListService;
        }

        #region Get All Materials
        /// <summary>
        /// Get all materials in the system
        /// </summary>
        /// <param name="page">Current page the user is on</param>
        /// <param name="pageSize">Number of entities you want to show</param>
        /// <param name="searchTerm">Search query</param>
        /// <param name="sortColumn">Column you want to sort</param>
        /// <param name="sortOrder">Sort column by ascending or descening</param>
        /// <returns>A list of all users</returns>
        /// <response code="200">Return all materials in the system</response>
        /// <response code="400">If no materials are in the system</response>
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
                var result = await _materialService.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

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

        #region Get All Gold Materials
        /// <summary>
        /// Get all gold materials in the system
        /// </summary>
        /// <returns>A list of gold materials</returns>
        /// <response code="200">Return a list of gold materials in the system</response>
        /// <response code="400">If the list is null</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpGet("golds")]
        public async Task<IActionResult> GetAllGoldMaterials()
        {
            try
            {
                var result = await _materialService.GetAllGoldMaterials();

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

        #region Add Material
        /// <summary>
        /// Add a material in the system
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///       "materialName": "Vàng 16K",
        ///       "materialPrice": {
        ///          "buyPrice": 11,
        ///          "sellPrice": 11
        ///        }            
        ///     }
        ///         
        /// </remarks> 
        /// <returns>Material that was created</returns>
        /// <response code="200">Material that was created</response>
        /// <response code="400">Failed validation</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateMaterialRequest createMaterialRequest)
        {
            try
            {
                await _materialService.AddAsync(createMaterialRequest);

                return Ok(createMaterialRequest);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Add Material Price
        /// <summary>
        /// Add new material price based on material Id in the system
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///       "buyPrice": 15,
        ///       "sellPrice": 15
        ///     }
        ///         
        /// </remarks> 
        /// <returns>Material price that was created</returns>
        /// <response code="200">Material that was created</response>
        /// <response code="400">Failed validation</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpPost("{id}/materialprices")]
        public async Task<IActionResult> AddAsync(int id, [FromBody] CreateMaterialPriceList createMaterialPriceList)
        {
            try
            {
                var material = await _materialService.GetByIdAsync(id);

                if (material == null) return NotFound(new
                {
                    ErrorMessage = $"Material with {id} does not exist"
                });

                var result = await _materialPriceListService.AddAsync(id, createMaterialPriceList);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Get Material By Id
        /// <summary>
        /// Get a material in the system
        /// </summary>
        /// <param name="id">Id of the material you want to get</param>
        /// <returns>A product</returns>
        /// <response code="200">Return a material in the system</response>
        /// <response code="400">If the material is null</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _materialService.GetByIdWithIncludeAsync(id);

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
                ErrorMessage = "Material does not exist"
            });
        }
        #endregion

        #region Update Material
        /// <summary>
        /// Update a material in the system
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
        /// <response code="400">Material does not exist</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Material material)
        {
            try
            {
                await _materialService.UpdateAsync(material);

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
