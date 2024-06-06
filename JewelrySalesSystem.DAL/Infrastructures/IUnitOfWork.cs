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

        /*Change here*/
        IRoleRepository Roles { get; }  

        Task CompleteAsync();
    }
}
