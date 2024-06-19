using JewelrySalesSystem.BAL.Models.Orders;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IOrderService
    {
        Task<List<GetOrderResponse>> GetAllAsync();

        Task<GetOrderResponse?> GetByIdWithIncludeAsync(int id);

        Task<CreateUpdateOrderRequest> AddAsync(CreateUpdateOrderRequest createRequest);

        Task UpdateAsync(CreateUpdateOrderRequest updateRequest);

        Task<GetOrderResponse?> GetByIdAsync(int id);

        Task DeleteAsync(int id);

        Task ChangeOrderStatusAsync(int id);

        Task CancelOrderAsync(int id);
    }
}
