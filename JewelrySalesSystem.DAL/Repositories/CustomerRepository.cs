using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JewelrySalesSystem.DAL.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(JewelryDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<Customer?> GetCustomerByNameAsync(string customerName)
            => await _dbSet
            .Where(c => c.FullName.Equals(customerName))
            .FirstOrDefaultAsync();

        public async Task<Customer?> GetCustomerByPhoneAsync(string phoneNumber)
        => await _dbSet
            .Where(c => c.PhoneNumber.Equals(phoneNumber))
            .FirstOrDefaultAsync();
    }
}
