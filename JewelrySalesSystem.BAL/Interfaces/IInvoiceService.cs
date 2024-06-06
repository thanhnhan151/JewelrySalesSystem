using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IInvoiceService
    {
        Task<PaginatedList<GetInvoiceResponse>> PaginationAsync
            (string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);

        Task<Invoice> AddAsync(Invoice invoice);

        Task UpdateAsync(Invoice invoice);

        Task<GetInvoiceResponse?> GetByIdWithIncludeAsync(int id);
    }
}
