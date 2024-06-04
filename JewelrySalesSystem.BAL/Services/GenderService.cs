using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using Microsoft.EntityFrameworkCore;

namespace JewelrySalesSystem.BAL.Services
{
    public class GenderService : IGenderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Gender> AddGender(Gender gender)
        {
            var result = _unitOfWork.Gender.AddEntity(gender);

            await _unitOfWork.CompleteAsync();

            return result;
        }

    }
}
