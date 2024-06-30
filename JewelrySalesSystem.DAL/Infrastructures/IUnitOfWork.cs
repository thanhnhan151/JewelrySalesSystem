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

        IColorRepository Colors { get; }

        ICustomerRepository Customers { get; }

        IProductMaterialRepository ProductMaterials { get; }

        Task CompleteAsync();
    }
}
