using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.DAL.Common;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IInvoiceService
    {
        Task<PaginatedList<GetInvoiceResponse>> PaginationAsync
            (string? invoiceStatus
            , string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , int page
            , int pageSize);

        Task<CreateInvoiceRequest> AddAsync(CreateInvoiceRequest createInvoiceRequest);

        Task<CreatePurchaseInvoiceRequest> AddPurchaseInvoiceAsync (CreatePurchaseInvoiceRequest createPurchaseInvoiceRequest);

        Task<UpdateInvoiceRequest> UpdateAsync(UpdateInvoiceRequest updateInvoiceRequest);

        Task<GetInvoiceResponse?> GetByIdAsync(int id);

        Task<GetInvoiceResponse?> GetByIdWithIncludeAsync(int id);

        Task DeleteInvoice(int id);

        Task ChangeInvoiceStatus(int id);

        Task CancelInvoice(int id);
    }
}
