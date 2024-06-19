using JewelrySalesSystem.BAL.Models.BuyInvoices;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IBuyInvoiceService
    {
        Task<List<GetBuyInvoiceResponse>> GetAllAsync();

        Task<GetBuyInvoiceResponse?> GetByIdAsync(int id);

        Task<GetBuyInvoiceResponse?> GetByIdWithIncludeAsync(int id);

        Task<CreateUpdateBuyInvoiceRequest> AddAsync(CreateUpdateBuyInvoiceRequest createRequest);

        Task UpdateAsync(CreateUpdateBuyInvoiceRequest updateRequest);

        Task ChangeBuyInvoiceStatusAsync(int id);

        Task CancelBuyInvoiceAsync(int id);
    }
}
