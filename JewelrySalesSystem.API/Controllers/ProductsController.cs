﻿using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Products;
using JewelrySalesSystem.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(
            ILogger<ProductsController> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        #region Get All Products
        /// <summary>
        /// Get all products in the system
        /// </summary>
        /// <param name="page">Current page the user is on</param>
        /// <param name="pageSize">Number of entities you want to show</param>
        /// <param name="searchTerm">Search query</param>
        /// <param name="sortColumn">Column you want to sort</param>
        /// <param name="sortOrder">Sort column by ascending or descening</param>
        /// <returns>A list of all users</returns>
        /// <response code="200">Return all products in the system</response>
        /// <response code="400">If no products are in the system</response>
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
                var result = await _productService.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

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

        #region Add Product
        /// <summary>
        /// Add a product in the system
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///       "productName": "Test Product",
        ///       "percentPriceRate": 10,
        ///       "productionCost": 100,
        ///       "featuredImage": "testurl",
        ///       "categoryId": 2,
        ///       "productTypeId": 2,
        ///       "genderId": 3,
        ///       "colourId": 4,
        ///       "gems" : [
        ///         7,
        ///         8
        ///       ],
        ///       "materials" : [
        ///         2,
        ///         5
        ///       ]
        ///     }
        ///         
        /// </remarks> 
        /// <returns>Product that was created</returns>
        /// <response code="200">Product that was created</response>
        /// <response code="400">Failed validation</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateProductRequest createProductRequest)
        {
            try
            {
                await _productService.AddAsync(createProductRequest);

                return Ok(createProductRequest);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Get Product By Id
        /// <summary>
        /// Get a product in the system
        /// </summary>
        /// <param name="id">Id of the product you want to get</param>
        /// <returns>A product</returns>
        /// <response code="200">Return a product in the system</response>
        /// <response code="400">If the product is null</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _productService.GetByIdWithIncludeAsync(id);

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
                ErrorMessage = $"Product with {id} does not exist"
            });
        }
        #endregion

        #region Update Product
        /// <summary>
        /// Update a product in the system
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///       "productId": 1
        ///       "productName": "Test Product",
        ///       "percentPriceRate": 10,
        ///       "productionCost": 100,
        ///       "featuredImage": "testurl",
        ///       "categoryId": 2,
        ///       "productTypeId": 2,
        ///       "genderId": 3,
        ///       "colourId": 4,
        ///       "gems" : [
        ///         1,
        ///         3
        ///       ],
        ///       "materials" : [
        ///         3,
        ///         4
        ///       ]
        ///     }
        ///         
        /// </remarks> 
        /// <returns>No content</returns>
        /// <response code="204">No content</response>
        /// <response code="400">Product does not exist</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateProductRequest updateProductRequest)
        {
            try
            {
                var result = await _productService.GetByIdAsync(updateProductRequest.ProductId);

                if (result == null) return NotFound(new
                {
                    ErrorMessage = $"Product with {updateProductRequest.ProductId} does not exist"
                });

                await _productService.UpdateAsync(updateProductRequest);

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Delete Product
        /// <summary>
        /// Change the product status in the system
        /// </summary>
        /// <param name="id">Id of the product you want to change</param>
        /// <response code="204">No Content</response>
        /// <response code="400">If the product is null</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _productService.GetByIdAsync(id);

                if (result == null) return NotFound(new
                {
                    ErrorMessage = $"Product with {id} does not exist"
                });

                await _productService.DeleteAsync(id);

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
