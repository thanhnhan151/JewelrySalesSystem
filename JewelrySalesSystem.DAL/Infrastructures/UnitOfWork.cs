using JewelrySalesSystem.DAL.Interfaces;
using JewelrySalesSystem.DAL.Persistence;
using JewelrySalesSystem.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace JewelrySalesSystem.DAL.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JewelryDbContext _context;

        private readonly ILogger _logger;

        public IUserRepository Users { get; private set; }

        public IWarrantyRepository Warranties { get; private set; }

        public IProductRepository Products { get; private set; }

        public IMaterialRepository Materials { get; private set; }

        public IInvoiceRepository Invoices { get; private set; }

        public IGemRepository Gems { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public IGenderRepository Genders { get; private set; }

        public IRoleRepository Roles { get; private set; }

        public IMaterialPriceListRepository MaterialPrices { get; private set; }

        public IProductTypeRepository ProductTypes { get; private set; }

        public IColourRepository Colours { get; private set; }

        public ICustomerRepository Customers { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public UnitOfWork(
            JewelryDbContext context,
            ILoggerFactory loggerFactory)
        {
            _context = context;

            _logger = loggerFactory.CreateLogger("logs");

            Users = new UserRepository(_context, _logger);

            Warranties = new WarrantyRepository(_context, _logger);

            Products = new ProductRepository(_context, _logger);

            Materials = new MaterialRepository(_context, _logger);

            Invoices = new InvoiceRepository(_context, _logger);

            Gems = new GemRepository(_context, _logger);

            Categories = new CategoryRepository(_context, _logger);

            Genders = new GenderRepository(_context, _logger);

            Roles = new RoleRepository(_context, _logger);

            MaterialPrices = new MaterialPriceRepository(_context, _logger);

            ProductTypes = new ProductTypeRepository(_context, _logger);

            Colours = new ColourRepository(_context, _logger);

            Customers = new CustomerRepository(_context, _logger);

            Orders = new OrderRepository(_context, _logger);
        }

        public async Task CompleteAsync() => await _context.SaveChangesAsync();
    }
}
