using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Orders;
using JewelrySalesSystem.DAL.Entities;
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

        public async Task<CreateUpdateOrderRequest> AddAsync(CreateUpdateOrderRequest createRequest)
        {
            var orderDetails = new List<OrderDetail>();

            if (createRequest.OrderDetails.Count > 0)
            {
                foreach (var item in createRequest.OrderDetails)
                {
                    orderDetails.Add(new()
                    {
                        ProductName = item.ProductName,
                        PurchaseTotal = item.PurchaseTotal,
                        PerDiscount = item.PerDiscount
                    });
                }
            }

            var order = new Order
            {
                CustomerName = createRequest.CustomerName,
                UserName = createRequest.UserName,
                Warranty = createRequest.Warranty,
                OrderDetails = orderDetails
            };

            _unitOfWork.Orders.AddEntity(order);

            await _unitOfWork.CompleteAsync();

            return createRequest;
        }

        public async Task<List<GetOrderResponse>> GetAllAsync() => _mapper.Map<List<GetOrderResponse>>(await _unitOfWork.Orders.GetAllAsync());

        public async Task<GetOrderResponse?> GetByIdWithIncludeAsync(int id) => _mapper.Map<GetOrderResponse>(await _unitOfWork.Orders.GetByIdWithInclude(id));

        public Task UpdateAsync(CreateUpdateOrderRequest updateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
