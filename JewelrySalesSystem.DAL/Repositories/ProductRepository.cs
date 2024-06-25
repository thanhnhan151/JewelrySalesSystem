using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(
            JewelryDbContext context,
            ILogger logger) : base(context, logger)
        {
        }

        public async Task<PaginatedList<Product>> PaginationAsync(
            string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize)
        {
            IQueryable<Product> productsQuery = _dbSet
                                                .OrderByDescending(p => p.ProductId)
                                                .Include(p => p.ProductGems)
                                                    .ThenInclude(g => g.Gem)
                                                        .ThenInclude(g => g.GemPrice)
                                                .Include(p => p.ProductMaterials)
                                                    .ThenInclude(m => m.Material)
                                                        .ThenInclude(g => g.MaterialPrices
                                                        .OrderByDescending(g => g.EffDate)
                                                        .Take(1))
                                                .Include(p => p.Category)
                                                .Include(p => p.ProductType)
                                                .Include(p => p.Gender)
                                                .Include(p => p.Colour);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(c =>
                    c.ProductName.Contains(searchTerm));
            }

            if (sortOrder?.ToLower() == "desc")
            {
                productsQuery = productsQuery.OrderByDescending(GetSortProperty(sortColumn));
            }
            else
            {
                productsQuery = productsQuery.OrderBy(GetSortProperty(sortColumn));
            }

            var products = await PaginatedList<Product>.CreateAsync(productsQuery, page, pageSize);

            return products;
        }

        private static Expression<Func<Product, object>> GetSortProperty(string? sortColumn)
        => sortColumn?.ToLower() switch
        {
            "name" => product => product.ProductName,
            //"dob" => product => product.DoB,
            _ => product => product.ProductId
        };

        public async Task<Product?> GetByIdWithIncludeAsync(int id)
        {
            var result = await _dbSet.Include(p => p.ProductGems)
                                    .ThenInclude(g => g.Gem)
                                        .ThenInclude(g => g.GemPrice)
                               .Include(p => p.ProductMaterials)
                                    .ThenInclude(m => m.Material)
                                        .ThenInclude(g => g.MaterialPrices
                                        .OrderByDescending(g => g.EffDate)
                                        .Take(1))
                               .Include(p => p.Category)
                               .Include(p => p.ProductType)
                               .Include(p => p.Gender)
                               .Include(p => p.Colour)
                               .FirstOrDefaultAsync(p => p.ProductId == id);

            if (result == null) return null;

            return result;
        }

        /*Delete Product*/
        //Change status of product = false
        public async Task DeleteProduct(int id)
        {
            var checkExistProduct = await _dbSet.FindAsync(id);

            if (checkExistProduct == null)
            {
                throw new Exception($"Product with {id} not found");
            }
            //Delete by change property status = false
            checkExistProduct.Status = false;
            _dbSet.Update(checkExistProduct);
        }

        public async Task UpdateProduct(Product product)
        {
            //Check exist
            var checkExistProductTask = _dbSet.FindAsync(product.ProductId);
            var checkExistProduct = await checkExistProductTask;
            if (checkExistProduct != null)
            {
                //Update data
                checkExistProduct.ProductName = product.ProductName;
                checkExistProduct.PercentPriceRate = product.PercentPriceRate;
                checkExistProduct.ProductionCost = product.ProductionCost;
                checkExistProduct.Status = product.Status;
                checkExistProduct.FeaturedImage = product.FeaturedImage;
                checkExistProduct.CategoryId = product.CategoryId;
                checkExistProduct.ProductTypeId = product.ProductTypeId;
                checkExistProduct.GenderId = product.GenderId;
                checkExistProduct.ColourId = product.ColourId;
                checkExistProduct.Weight = product.Weight;


                var existProductGems = await _context.ProductGems.Where(pe => pe.ProductId == product.ProductId).ToListAsync();

                var existingProductGemsDict = existProductGems.ToDictionary(pg => pg.GemId, pg => pg);


                var newProductGems = product.ProductGems.Where(g => !existingProductGemsDict.ContainsKey(g.GemId))
                                                        .Select(g => new ProductGem
                                                        {
                                                            GemId = g.GemId,
                                                            ProductId = product.ProductId
                                                        }).ToList();
                var removedProductGems = existingProductGemsDict.Values.Where(eg => !product.ProductGems.Any(g => g.GemId == eg.GemId)).ToList();
                _context.ProductGems.RemoveRange(removedProductGems);
                _context.ProductGems.AddRange(newProductGems);


                var existProductMaterials = await _context.ProductMaterials.Where(ma => ma.ProductId == product.ProductId).ToListAsync();
                var existingProductMaterialsDict = existProductMaterials.ToDictionary(pm => pm.MaterialId, pm => pm);
                var newProductMaterials = product.ProductMaterials.Where(m => !existProductMaterials.Any(em => em.MaterialId == m.MaterialId))
                                                                  .Select(m => new ProductMaterial
                                                                  {
                                                                      Id = m.Id,
                                                                      MaterialId = m.MaterialId,
                                                                      ProductId = product.ProductId
                                                                  });
                var removedProductMaterials = existingProductMaterialsDict.Values.Where(em => !product.ProductMaterials.Any(m => m.MaterialId == em.MaterialId)).ToList();
                _context.ProductMaterials.RemoveRange(removedProductMaterials);
                _context.ProductMaterials.AddRange(newProductMaterials);



            }
            else
            {
                throw new Exception($"Product with ID = {product.ProductId} not found!");
            }
            _dbSet.Update(checkExistProduct);
        }
    }
}
