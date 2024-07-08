using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.DAL.Common;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IInvoiceService
    {
        Task<PaginatedList<GetInvoiceResponse>> PaginationAsync
            (string? invoiceStatus
            , string? invoiceType
            , string? searchTerm
            , string? sortColumn
            , string? sortOrder
            , bool isActive
            , int page
            , int pageSize);

        Task<CreateInvoiceRequest> AddAsync(CreateInvoiceRequest createInvoiceRequest);

        Task<CreatePurchaseInvoiceRequest> AddPurchaseInvoiceAsync (CreatePurchaseInvoiceRequest createPurchaseInvoiceRequest);

        Task<UpdateInvoiceRequest> UpdateAsync(UpdateInvoiceRequest updateInvoiceRequest);

        Task<GetInvoiceResponse?> GetByIdAsync(int id);

        Task<GetInvoiceResponse?> GetByIdWithIncludeAsync(int id);

        Task DeleteInvoice(int id);

        Task ChangeInvoiceStatus(int id);

        Task ChangePendingToDraft(int id);

        Task CancelInvoice(int id);

        Task<byte[]> GenerateInvoicePdf(int invoiceId);

        Task<byte[]> GenerateInvoiceExcel(int month, int year);
    }
}
