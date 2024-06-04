using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IGenderService
    {
        Task<Gender> AddGender(Gender gender);
    }
}
