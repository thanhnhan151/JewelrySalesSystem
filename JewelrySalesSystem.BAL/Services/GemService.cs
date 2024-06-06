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

        public async Task<Gem> AddAsync(Gem gem)
        {
            var result = _unitOfWork.Gems.AddEntity(gem);

            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task UpdateAsync(Gem gem)
        {
            _unitOfWork.Gems.UpdateEntity(gem);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetGemResponse?> GetByIdWithIncludeAsync(int id) => _mapper.Map<GetGemResponse>(await _unitOfWork.Gems.GetByIdWithIncludeAsync(id));
    }
}
