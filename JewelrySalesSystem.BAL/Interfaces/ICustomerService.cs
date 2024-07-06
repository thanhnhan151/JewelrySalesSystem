using JewelrySalesSystem.BAL.Models.Customers;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface ICustomerService
    {
        Task<GetCustomerResponse?> GetCustomerByNameAsync(string customerName);
        Task<GetCustomerResponse?> GetCustomerByPhoneAsync(string phoneNumber);
    }
}
