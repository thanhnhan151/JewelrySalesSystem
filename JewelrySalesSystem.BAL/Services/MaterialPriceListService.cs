using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Genders;
using JewelrySalesSystem.BAL.Models.MaterialPriceList;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesSystem.BAL.Services
{
    public class MaterialPriceListService : IMaterialPriceListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaterialPriceListService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateMaterialPriceList> AddAsync(CreateMaterialPriceList materialPriceList)
        {
            var result =  _unitOfWork.MaterialPriceList.AddEntity(_mapper.Map<MaterialPriceList>(materialPriceList));

            await _unitOfWork.CompleteAsync();
            var createMaterialPriceList = _mapper.Map<CreateMaterialPriceList>(result);
            return createMaterialPriceList ;
        }
    }
}
