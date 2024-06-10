using JewelrySalesSystem.BAL.Models.MaterialPriceList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IMaterialPriceListService
    {
        Task<CreateMaterialPriceList> AddAsync (CreateMaterialPriceList materialPriceList);
    }
}
