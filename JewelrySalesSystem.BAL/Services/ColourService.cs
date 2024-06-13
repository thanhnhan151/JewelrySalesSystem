using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Colours;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class ColourService : IColourService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ColourService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetColourResponse>> GetAllAsync() => _mapper.Map<List<GetColourResponse>>(await _unitOfWork.Colours.GetAllAsync());

        public async Task<GetColourResponse?> GetByIdAsync(int id) => _mapper.Map<GetColourResponse>(await _unitOfWork.Colours.GetEntityByIdAsync(id));
    }
}
