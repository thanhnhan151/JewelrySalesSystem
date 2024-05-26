using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> LoginAsync(string email, string passWord)
        {
            return await _unitOfWork.Users.LoginAsync(email, passWord);
        }
    }
}
