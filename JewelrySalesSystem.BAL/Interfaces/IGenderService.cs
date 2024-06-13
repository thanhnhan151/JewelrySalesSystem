using JewelrySalesSystem.BAL.Models.Genders;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IGenderService
    {
        Task<CreateGenderRequest> AddGender(CreateGenderRequest gender);
    }
}
