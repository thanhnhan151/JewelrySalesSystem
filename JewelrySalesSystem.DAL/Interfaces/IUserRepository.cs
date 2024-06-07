using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> LoginAsync(string userName, string passWord);

        Task<PaginatedList<User>> PaginationAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);

        Task<User?> GetByIdWithIncludeAsync(int id);

        Task<User>AddAsync(User user);
    }
}
