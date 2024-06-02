using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IUserService
    {
        Task<User?> LoginAsync(string email, string passWord);

        Task<PaginatedList<User>> PaginationAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);

        Task<User> AddAsync(User user);

        Task UpdateAsync(User user);

        Task<User?> GetByIdAsync(int id);
    }
}
