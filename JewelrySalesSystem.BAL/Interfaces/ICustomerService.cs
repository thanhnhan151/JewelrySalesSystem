using JewelrySalesSystem.BAL.Models.Customers;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface ICustomerService
    {
        Task<GetCustomerPointResponse?> GetCustomerPointByNameAsync(string customerName);
    }
}
