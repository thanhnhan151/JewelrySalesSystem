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

        public async Task<bool> LoginAsync(string email, string passWord)
            => await _unitOfWork.Users.LoginAsync(email, passWord);

        public async Task<PaginatedList<User>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => await _unitOfWork.Users.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize);
    }
}
