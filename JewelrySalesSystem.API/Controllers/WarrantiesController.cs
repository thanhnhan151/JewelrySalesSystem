using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Users;
using JewelrySalesSystem.BAL.Models.Warranties;
using JewelrySalesSystem.BAL.Services;
using JewelrySalesSystem.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/warranties")]
    [ApiController]
    public class WarrantiesController : ControllerBase
    {
        private readonly ILogger<WarrantiesController> _logger;
        private readonly IWarrantyService _warrantyService;

        public WarrantiesController(
            ILogger<WarrantiesController> logger,
            IWarrantyService warrantyService)
        {
            _logger = logger;
            _warrantyService = warrantyService;
        }

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
                var result = await _warrantyService.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

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

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _warrantyService.GetWarrantyById(id);
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

        

        #region Update Warranty
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateWarrantyRequest updateWarrantyRequest)
        {
            try
            {
                //var warranty = await _warrantyService.GetById(updateWarrantyRequest.WarrantyId);
                //if (warranty == null)
                //    return NotFound("Warranty not found.");

                await _warrantyService.UpdateAsync(updateWarrantyRequest);

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
