using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Gems;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class GemService : IGemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GemService(
            IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<GetGemResponse>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => _mapper.Map<PaginatedList<GetGemResponse>>(await _unitOfWork.Gems.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize));

        public async Task<CreateGemRequest> AddAsync(CreateGemRequest createGemRequest)
        {
            var gem = new Gem
            {
                GemName = createGemRequest.GemName,
                Origin = createGemRequest.Origin,
                CaratWeight = createGemRequest.CaratWeight,
                Colour = createGemRequest.Colour,
                Clarity = createGemRequest.Clarity,
                Cut = createGemRequest.Cut,
                GemPrices = new List<GemPriceList>
                {
                    new GemPriceList
                    {
                        CaratWeightPrice = createGemRequest.GemPrice.CaratWeightPrice,
                        ColourPrice = createGemRequest.GemPrice.ColourPrice,
                        ClarityPrice = createGemRequest.GemPrice.ClarityPrice,
                        CutPrice = createGemRequest.GemPrice.CutPrice,
                        EffDate = DateTime.Now
                    }
                }
            };

            var result = _unitOfWork.Gems.AddEntity(gem);

            await _unitOfWork.CompleteAsync();

            return createGemRequest;
        }

        public async Task UpdateAsync(Gem gem)
        {
            _unitOfWork.Gems.UpdateEntity(gem);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetGemResponse?> GetByIdWithIncludeAsync(int id) => _mapper.Map<GetGemResponse>(await _unitOfWork.Gems.GetByIdWithIncludeAsync(id));
    }
}
