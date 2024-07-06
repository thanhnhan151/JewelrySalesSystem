using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Customers;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetCustomerResponse?> GetCustomerByNameAsync(string customerName)
        => _mapper.Map<GetCustomerResponse>(await _unitOfWork.Customers.GetCustomerByNameAsync(customerName));

        public async Task<GetCustomerResponse?> GetCustomerByPhoneAsync(string phoneNumber)
        => _mapper.Map<GetCustomerResponse>(await _unitOfWork.Customers.GetCustomerByNameAsync(phoneNumber));
    }
}
