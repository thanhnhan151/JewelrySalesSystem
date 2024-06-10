using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.DAL.Interfaces
{
    public interface IGemPriceListRepository : IGenericRepository<GemPriceList>
    {
        Task<GemPriceList> AddGemPrice(GemPriceList gemPrice);
    }
}
