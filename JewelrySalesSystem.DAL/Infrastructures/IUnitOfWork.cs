using JewelrySalesSystem.DAL.Interfaces;

namespace JewelrySalesSystem.DAL.Infrastructures
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        IWarrantyRepository Warranties { get; }

        IProductRepository Products { get; }

        IMaterialRepository Materials { get; }

        IInvoiceRepository Invoices { get; }

        IGemRepository Gems { get; }

        ICategoryRepository Categories { get; }

        IGenderRepository Genders {  get; }

        IRoleRepository Roles { get; } 

        IMaterialPriceListRepository MaterialPrices { get; }

        IProductTypeRepository ProductTypes { get; }

        IColourRepository Colours { get; }

        ICustomerRepository Customers { get; }

        IOrderRepository Orders { get; }

        IBuyInvoiceRepository BuyInvoices { get; }

        Task CompleteAsync();
    }
}
