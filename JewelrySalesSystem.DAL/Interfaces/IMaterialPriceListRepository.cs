using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesSystem.DAL.Interfaces
{
    public interface IMaterialPriceListRepository : IGenericRepository<MaterialPriceList>
    {
        Task<MaterialPriceList> CreateMaterialPrice(MaterialPriceList materialPrice);
    }
}
