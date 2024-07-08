using JewelrySalesSystem.BAL.Models.Counters;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface ICounterService
    {
        Task<List<GetCounterResponse>> GetAllAsync();
    }
}
