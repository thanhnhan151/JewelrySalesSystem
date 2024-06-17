using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Orders;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetOrderResponse>> GetAllAsync() => _mapper.Map<List<GetOrderResponse>>(await _unitOfWork.Orders.GetAllAsync());

        public async Task<GetOrderResponse?> GetByIdWithIncludeAsync(int id) => _mapper.Map<GetOrderResponse>(await _unitOfWork.Orders.GetByIdWithInclude(id));
    }
}
