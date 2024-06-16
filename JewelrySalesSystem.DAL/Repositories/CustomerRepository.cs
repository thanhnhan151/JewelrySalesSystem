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

        public async Task<Customer?> GetCustomerPointByNameAsync(string customerName)
            => await _dbSet.FirstOrDefaultAsync(c => c.FullName.Equals(customerName));
    }
}
