using JewelrySalesSystem.BAL.Models.Orders;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IOrderService
    {
        Task<List<GetOrderResponse>> GetAllAsync();

        Task<GetOrderResponse?> GetByIdWithIncludeAsync(int id);

        Task<CreateUpdateOrderRequest> AddAsync(CreateUpdateOrderRequest createRequest);

        Task UpdateAsync(CreateUpdateOrderRequest updateRequest);
    }
}
