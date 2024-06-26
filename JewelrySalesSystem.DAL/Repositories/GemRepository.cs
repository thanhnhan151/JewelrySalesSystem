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
    public class GemRepository : GenericRepository<Gem>, IGemRepository
    {
        public GemRepository(
            JewelryDbContext context,
            ILogger logger) : base(context, logger)
        {
        }

        public async Task<PaginatedList<Gem>> PaginationAsync(
            string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize)
        {
            IQueryable<Gem> gemsQuery = _dbSet
                .OrderByDescending(g => g.GemId)
                .Include(g => g.GemPrice);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                gemsQuery = gemsQuery.Where(c =>
                    c.GemName.Contains(searchTerm) ||
                    c.Origin.Contains(searchTerm));
            }

            if (sortOrder?.ToLower() == "desc")
            {
                gemsQuery = gemsQuery.OrderByDescending(GetSortProperty(sortColumn));
            }
            else
            {
                gemsQuery = gemsQuery.OrderBy(GetSortProperty(sortColumn));
            }

            var gems = await PaginatedList<Gem>.CreateAsync(gemsQuery, page, pageSize);

            return gems;
        }

        private static Expression<Func<Gem, object>> GetSortProperty(string? sortColumn)
        => sortColumn?.ToLower() switch
        {
            "name" => gem => gem.GemName,
            //"dob" => gem => gem.DoB,
            _ => gem => gem.GemId
        };

        public async Task<Gem?> GetByIdWithIncludeAsync(int id)
        {
            var result = await _dbSet
                .Include(g => g.GemPrice)
                .FirstOrDefaultAsync(g => g.GemId == id);

            if (result == null) return null;
            return result;
        }

        public async Task UpdateGem(Gem gem)
        {
            //Check exist
            var checkExistGemTask = _dbSet.FindAsync(gem.GemId);
            var checkExistGem = await checkExistGemTask;
            if (checkExistGem != null)
            {
                //Update data
                checkExistGem.GemName = gem.GemName;
                checkExistGem.FeaturedImage = gem.FeaturedImage;
                checkExistGem.Origin = gem.Origin;
                checkExistGem.CaratWeight = gem.CaratWeight;
                checkExistGem.Colour = gem.Colour;
                checkExistGem.Clarity = gem.Clarity;
                checkExistGem.Cut = gem.Cut;
                var gemPrice = await _context.GemPrices.FirstOrDefaultAsync(p => p.GemId == gem.GemId);
                if (gemPrice == null)
                {
                    gemPrice = new GemPriceList
                    {
                        GemId = gem.GemId,
                        CaratWeightPrice = gem.GemPrice.CaratWeightPrice,
                        ColourPrice = gem.GemPrice.ColourPrice,
                        ClarityPrice = gem.GemPrice.ClarityPrice,
                        CutPrice = gem.GemPrice.CutPrice,

                    };
                    _context.GemPrices.Add(gemPrice);
                }
                else
                {
                    gemPrice.CaratWeightPrice = gem.GemPrice.CaratWeightPrice;
                    gemPrice.ColourPrice = gem.GemPrice.ColourPrice;
                    gemPrice.ClarityPrice = gem.GemPrice.ClarityPrice;
                    gemPrice.CutPrice = gem.GemPrice.CutPrice;
                    _context.GemPrices.Update(gemPrice);

                }

            }
            else
            {
                throw new Exception($"Gem with ID = {gem.GemId} not found!");
            }
            _dbSet.Update(checkExistGem);
        }

        public async Task<Gem?> GetByNameWithIncludeAsync(string name)
        {
            var result = await _dbSet
                .Include(g => g.GemPrice)
                .FirstOrDefaultAsync(g => g.GemName.Equals(name));

            if (result == null) return null;
            return result;
        }
    }
}
