using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.BuyInvoices;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class BuyInvoiceService : IBuyInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BuyInvoiceService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateUpdateBuyInvoiceRequest> AddAsync(CreateUpdateBuyInvoiceRequest createRequest)
        {
            var orderDetails = new List<OrderDetail>();

            if (createRequest.Items.Count > 0)
            {
                foreach (var item in createRequest.Items)
                {
                    orderDetails.Add(new()
                    {
                        ProductName = item.ProductName,
                        PurchaseTotal = item.PurchaseTotal,
                        PerDiscount = item.PerDiscount
                    });
                }
            }

            var buyInvoice = new BuyInvoice
            {
                CustomerName = createRequest.CustomerName,
                UserName = createRequest.UserName,
                Items = orderDetails
            };

            _unitOfWork.BuyInvoices.AddEntity(buyInvoice);

            await _unitOfWork.CompleteAsync();

            return createRequest;
        }

        public async Task<List<GetBuyInvoiceResponse>> GetAllAsync() => _mapper.Map<List<GetBuyInvoiceResponse>>(await _unitOfWork.BuyInvoices.GetAllAsync());

        public async Task<GetBuyInvoiceResponse?> GetByIdWithIncludeAsync(int id) => _mapper.Map<GetBuyInvoiceResponse>(await _unitOfWork.BuyInvoices.GetByIdWithInclude(id));

        public Task UpdateAsync(CreateUpdateBuyInvoiceRequest updateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
