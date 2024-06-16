using JewelrySalesSystem.BAL.Models.Colours;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IColourService
    {
        Task<List<GetColourResponse>> GetAllAsync();

        Task<GetColourResponse?> GetByIdAsync(int id);
    }
}
