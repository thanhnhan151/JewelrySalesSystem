using JewelrySalesSystem.BAL.Models.BuyInvoices;
using JewelrySalesSystem.BAL.Models.Orders;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IBuyInvoiceService
    {
        Task<List<GetBuyInvoiceResponse>> GetAllAsync();

        Task<GetBuyInvoiceResponse?> GetByIdWithIncludeAsync(int id);

        Task<CreateUpdateBuyInvoiceRequest> AddAsync(CreateUpdateBuyInvoiceRequest createRequest);

        Task UpdateAsync(CreateUpdateBuyInvoiceRequest updateRequest);
    }
}
