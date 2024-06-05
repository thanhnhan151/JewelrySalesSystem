using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Materials;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaterialService(
            IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<GetMaterialResponse>> PaginationAsync(
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            int page,
            int pageSize)
        => _mapper.Map<PaginatedList<GetMaterialResponse>>(await _unitOfWork.Materials.PaginationAsync(searchTerm, sortColumn, sortOrder, page, pageSize));

        public async Task<Material> AddAsync(Material material)
        {
            var result = _unitOfWork.Materials.AddEntity(material);

            await _unitOfWork.CompleteAsync();

            return result;
        }

        public async Task UpdateAsync(Material material)
        {
            _unitOfWork.Materials.UpdateEntity(material);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<GetMaterialResponse?> GetByIdWithIncludeAsync(int id) => _mapper.Map<GetMaterialResponse>(await _unitOfWork.Materials.GetByIdWithIncludeAsync(id));
    }
}
