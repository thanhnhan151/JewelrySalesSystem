using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.MaterialPriceList;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class MaterialPriceListService : IMaterialPriceListService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MaterialPriceListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateMaterialPriceList> AddAsync(int id, CreateMaterialPriceList materialPriceList)
        {
            var materialPrice = new MaterialPriceList
            {
                SellPrice = materialPriceList.SellPrice,
                BuyPrice = materialPriceList.BuyPrice,
                MaterialId = id,
                EffDate = DateTime.Now
            };

            var result = _unitOfWork.MaterialPrices.AddEntity(materialPrice);

            await _unitOfWork.CompleteAsync();
            return materialPriceList;
        }
    }
}
