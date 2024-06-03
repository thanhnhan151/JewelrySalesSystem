using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
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

        public async Task<User?> LoginAsync(string userName, string passWord)
            => await _unitOfWork.Users.LoginAsync(userName, passWord);

        public async Task<PaginatedList<User>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => await _unitOfWork.Users.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);

        public async Task<User> AddAsync(User user)
        {
            var result = _unitOfWork.Users.AddEntity(user);

            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task UpdateAsync(User user)
        {
            _unitOfWork.Users.UpdateEntity(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<User?> GetByIdAsync(int id) => await _unitOfWork.Users.GetByIdAsync(id);
    }
}
