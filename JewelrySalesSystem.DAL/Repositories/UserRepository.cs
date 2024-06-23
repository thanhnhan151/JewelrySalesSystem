using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(
            JewelryDbContext context,
            ILogger logger
            ) : base(context, logger)
        {
        }

        public async Task<User?> LoginAsync(string userName, string passWord)
        {
            try
            {
                var account = await _dbSet.Where(a => a.UserName == userName
                                                 && a.Password == passWord)
                                          .Include(u => u.Role)
                                          .FirstOrDefaultAsync();

                if (account != null)
                    return account;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} LoginAsync method error", typeof(UserRepository));
                return new User();
            }
        }

        public async Task<PaginatedList<User>> PaginationAsync(
            string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize)
        {
            IQueryable<User> usersQuery = _dbSet
                .OrderByDescending(u => u.UserId)
                .Include(u => u.Role);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                usersQuery = usersQuery.Where(c =>
                    c.FullName.Contains(searchTerm) ||
                    c.PhoneNumber.Contains(searchTerm) ||
                    c.Email.Contains(searchTerm));
            }

            if (sortOrder?.ToLower() == "desc")
            {
                usersQuery = usersQuery.OrderByDescending(GetSortProperty(sortColumn));
            }
            else
            {
                usersQuery = usersQuery.OrderBy(GetSortProperty(sortColumn));
            }

            var users = await PaginatedList<User>.CreateAsync(usersQuery, page, pageSize);

            return users;
        }

        private static Expression<Func<User, object>> GetSortProperty(string? sortColumn)
        => sortColumn?.ToLower() switch
        {
            "name" => user => user.FullName,
            //"dob" => user => user.DoB,
            _ => user => user.UserId
        };

        public async Task<User?> GetByIdWithIncludeAsync(int id)
        {
            var result = await _dbSet.Include(u => u.Role)
                               .FirstOrDefaultAsync(u => u.UserId == id);

            if (result == null) { return null; }

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var checkExistUser = await _dbSet.FindAsync(id);

            if (checkExistUser == null)
            {
                throw new Exception($"User with {id} not found");
            }
            //Delete by change property status = false
            checkExistUser.Status = false;
            _dbSet.Update(checkExistUser);
        }
    }
}
