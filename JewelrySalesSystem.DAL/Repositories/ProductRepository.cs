﻿using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
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
            IQueryable<Product> productsQuery = _dbSet;

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
    }
}
