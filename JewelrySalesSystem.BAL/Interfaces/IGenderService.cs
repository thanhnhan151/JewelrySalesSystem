using JewelrySalesSystem.BAL.Models.Gender;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IGenderService
    {
        Task<GenderModel> AddGender(GenderModel gender);
    }
}
