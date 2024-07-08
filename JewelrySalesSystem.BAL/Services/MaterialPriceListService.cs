using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.MaterialPriceList;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class MaterialPriceListService : IMaterialPriceListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaterialPriceListService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<CreateMaterialPriceList>> AddAsync(List<CreateMaterialPriceList> prices)
        {
            var materialPrices = _mapper.Map<List<MaterialPriceList>>(prices);

            await _unitOfWork.MaterialPrices.AddEntities(materialPrices);

            var products = await _unitOfWork.Products.GetJewelryAndMaterialProducts();

            foreach (var item in products)
            {
                if (item.ProductTypeId == 2)
                {
                    var material = await _unitOfWork.Materials.GetByNameWithIncludeAsync(item.ProductName);

                    if (material != null)
                    {
                        var price = await _unitOfWork.MaterialPrices.GetNewestMaterialPriceByMaterialIdAsync(material.MaterialId);

                        item.ProductPrice = price.SellPrice;
                    }
                }
                else
                {
                    float productPrice = 0;

                    productPrice += item.ProductionCost;

                    if (item.ProductGems.Count > 0)
                    {
                        foreach (var temp in item.ProductGems)
                        {
                            var gem = await _unitOfWork.Gems.GetEntityByIdAsync(temp.GemId);

                            if (gem != null)
                            {
                                float price = await _unitOfWork.Gems.GetGemPriceAsync(gem);

                                float shapePriceRate = await _unitOfWork.Gems.GetShapePriceRateAsync(gem.ShapeId);

                                productPrice += price * (1 + shapePriceRate / 100);
                            }
                        }
                    }

                    if (item.ProductMaterials.Count > 0)
                    {
                        foreach (var temp in item.ProductMaterials)
                        {
                            var material = await _unitOfWork.Materials.GetByIdWithIncludeAsync(temp.MaterialId);

                            if (material != null)
                            {
                                var materialPrice = material.MaterialPrices.SingleOrDefault();

                                if (materialPrice != null) productPrice += ((temp.Weight / 100) * materialPrice.SellPrice) * 375 / 100;
                            }
                        }
                    }
                    productPrice += (productPrice * (item.PercentPriceRate) / 100);

                    item.ProductPrice = productPrice;
                }
            }

            _unitOfWork.Products.UpdateAllProducts(products);

            await _unitOfWork.CompleteAsync();
            return prices;
        }
    }
}
