using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.DAL.Interfaces
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        Task<PaginatedList<Invoice>> PaginationAsync
            (string? invoiceStatus
            , string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);

        Task<Invoice?> GetByIdWithIncludeAsync(int id);

        Task DeleteById(int id);

        Task UpdateInvoice(Invoice invoice);
    }
}
