using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class MaterialPriceRepository : GenericRepository<MaterialPriceList>, IMaterialPriceListRepository
    {
        public MaterialPriceRepository(
           JewelryDbContext context,
           ILogger logger) : base(context, logger)
        {

        }

        public async Task<MaterialPriceList> CreateMaterialPrice(MaterialPriceList materialPrice)
        {
            _context.MaterialPrices.Add(materialPrice);

            await _context.SaveChangesAsync();
            return materialPrice;

        }
    }
}
