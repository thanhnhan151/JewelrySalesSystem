using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Categories;
using JewelrySalesSystem.BAL.Models.ProductType;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //changes here
        public async Task<List<GetAllProductType>> GetAllAsync()
        {
            var productTypes = await _unitOfWork.ProductTypes.GetAllAsync();
            return _mapper.Map<List<GetAllProductType>>(productTypes);
        }

        public async Task<GetProductTypeById> GetProductTypeByIdAsync(int id)
        {
            var result = await _unitOfWork.ProductTypes.GetEntityByIdAsync(id);
            return _mapper.Map<GetProductTypeById>(result);
        }
    }
}
