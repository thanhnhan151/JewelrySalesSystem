using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.GemPriceLists;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class GemPriceListService : IGemPriceListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GemPriceListService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateGemPriceRequest> AddGemPrice(CreateGemPriceRequest createGemPriceRequest)
        {
            var result = _unitOfWork.GemPrices.AddEntity(_mapper.Map<GemPriceList>(createGemPriceRequest));
            await _unitOfWork.CompleteAsync();
            var newGemPrice = _mapper.Map<CreateGemPriceRequest>(result);
            return newGemPrice;
        }
    }
}
