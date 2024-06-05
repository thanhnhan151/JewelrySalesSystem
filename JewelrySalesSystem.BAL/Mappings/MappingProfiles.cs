using AutoMapper;
using JewelrySalesSystem.BAL.Models.Gems;
using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.BAL.Models.Materials;
using JewelrySalesSystem.BAL.Models.Products;
using JewelrySalesSystem.BAL.Models.Users;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region User
            CreateMap<User, GetUserResponse>();
            CreateMap<PaginatedList<User>, PaginatedList<GetUserResponse>>();
            CreateMap<CreateUserRequest, User>();
            CreateMap<UpdateUserRequest, User>();
            #endregion

            #region Product
            CreateMap<Product, GetProductResponse>()
                .ForMember(p => p.Materials, p => p.MapFrom(p => p.ProductMaterials
                .Select(y => y.Material).ToList()))

                .ForMember(p => p.Gems, p => p.MapFrom(p => p.ProductGems
                .Select(y => y.Gem).ToList()));
            CreateMap<PaginatedList<Product>, PaginatedList<GetProductResponse>>();
            #endregion

            #region Gem
            CreateMap<Gem, GemItem>();

            CreateMap<Gem, GetGemResponse>()
                .ForMember(g => g.GemPrice, g => g.MapFrom(m => m.GemPrices.SingleOrDefault()));

            CreateMap<PaginatedList<Gem>, PaginatedList<GetGemResponse>>();

            CreateMap<GemPriceList, GemPrice>();
            #endregion

            #region Material
            CreateMap<Material, MaterialItem>();

            CreateMap<Material, GetMaterialResponse>()
                .ForMember(m => m.MaterialPrice, m => m.MapFrom(m => m.MaterialPrices.SingleOrDefault()));

            CreateMap<PaginatedList<Material>, PaginatedList<GetMaterialResponse>>();

            CreateMap<MaterialPriceList, MaterialPrice>();
            #endregion

            #region Invoice
            CreateMap<Invoice, GetInvoiceResponse>()
                .ForMember(i => i.Items, i => i.MapFrom(i => i.InvoiceDetails))

                .ForMember(i => i.Total, i => i.MapFrom(i => i.InvoiceDetails
                .Sum(i => i.ProductPrice)));

            CreateMap<PaginatedList<Invoice>, PaginatedList<GetInvoiceResponse>>();

            CreateMap<InvoiceDetail, InvoiceItem>()
                .ForMember(i => i.ProductName, i => i.MapFrom(i => i.Product.ProductName));
            #endregion
        }
    }
}
