using JewelrySalesSystem.BAL.Models.GemPriceLists;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IGemPriceListService
    {
        Task<CreateGemPriceRequest> AddGemPrice(CreateGemPriceRequest createGemPriceRequest);
    }
}
