using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Customers;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> GetCustomerByNameAsync(string customerName)
        {
            return await _unitOfWork.Customers.GetCustomerByNameAsync(customerName);
        }

        public async Task<GetCustomerPointResponse?> GetCustomerPointByNameAsync(string customerName)
        {
            var customer = await _unitOfWork.Customers.GetCustomerPointByNameAsync(customerName);

            if (customer != null)
            {
                var result = new GetCustomerPointResponse
                {
                    Point = customer.Point
                };

                return result;
            }
            return null;
        }
    }
}
