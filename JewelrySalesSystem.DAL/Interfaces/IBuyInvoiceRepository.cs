using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.DAL.Interfaces
{
    public interface IBuyInvoiceRepository : IGenericRepository<BuyInvoice>
    {
        Task<List<BuyInvoice>> GetAllAsync();
        Task<BuyInvoice?> GetByIdWithInclude(int id);
    }
}
