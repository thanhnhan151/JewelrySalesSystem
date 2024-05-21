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

        public async Task<bool> LoginAsync(string email, string passWord)
        {
            try
            {
                var account = await _dbSet.Where(a => a.Email == email
                                                 && a.Password == passWord)
                                          .FirstOrDefaultAsync();

                if (account != null)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} LoginAsync method error", typeof(UserRepository));
                return false;
            }
        }

        public async Task<PaginatedList<User>> PaginationAsync(
            string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize)
        {
            IQueryable<User> usersQuery = _dbSet;

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
    }
}
