using JewelrySalesSystem.BAL.Models.Genders;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IGenderService
    {
        Task<GenderModel> AddGender(GenderModel gender);
    }
}
