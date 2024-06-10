using AutoMapper;
using JewelrySalesSystem.BAL.Models.Categories;
using JewelrySalesSystem.BAL.Models.GemPriceLists;
using JewelrySalesSystem.BAL.Models.Gems;
using JewelrySalesSystem.BAL.Models.Genders;
using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.BAL.Models.Materials;
using JewelrySalesSystem.BAL.Models.Products;
using JewelrySalesSystem.BAL.Models.Roles;
using JewelrySalesSystem.BAL.Models.Users;
using JewelrySalesSystem.BAL.Models.Warranties;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Role
            CreateMap<RoleViewModel, Role>().ReverseMap();
            #endregion

            #region Gender
            CreateMap<GenderModel, Gender>().ReverseMap();
            #endregion

            #region User
            CreateMap<User, GetUserResponse>()
                .ForMember(u => u.Role, u => u.MapFrom(u => u.Role.RoleName));

            CreateMap<PaginatedList<User>, PaginatedList<GetUserResponse>>();

            CreateMap<CreateUserRequest, User>();

            CreateMap<UpdateUserRequest, User>();
            CreateMap<DeleteUserRequest, User>().ReverseMap();
            #endregion

            #region Product
            CreateMap<Product, GetProductResponse>()
                .ForMember(p => p.Materials, p => p.MapFrom(p => p.ProductMaterials
                .Select(y => y.Material).ToList()))

                .ForMember(p => p.Gems, p => p.MapFrom(p => p.ProductGems
                .Select(y => y.Gem).ToList()))
                
                .ForMember(p => p.Category, p => p.MapFrom(p => p.Category.CategoryName))

                .ForMember(p => p.ProductType, p => p.MapFrom(p => p.ProductType.Name))

                .ForMember(p => p.Gender, p => p.MapFrom(p => p.Gender.GenderName))

                .ForMember(p => p.Colour, p => p.MapFrom(p => p.Colour.ColourName));

            CreateMap<Product, ProductCategoryResponse>()
                .ForMember(p => p.ProductType, p => p.MapFrom(p => p.ProductType.Name))

                .ForMember(p => p.Gender, p => p.MapFrom(p => p.Gender.GenderName))

                .ForMember(p => p.Colour, p => p.MapFrom(p => p.Colour.ColourName));

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
                .Sum(i => i.ProductPrice)))
                
                .ForMember(i => i.CustomerName, i => i.MapFrom(i => i.Customer.FullName))

                .ForMember(i => i.UserName, i => i.MapFrom(i => i.User.UserName))

                .ForMember(i => i.Warranty, i => i.MapFrom(i => i.Warranty.Description));

            CreateMap<PaginatedList<Invoice>, PaginatedList<GetInvoiceResponse>>();

            CreateMap<InvoiceDetail, InvoiceItem>()
                .ForMember(i => i.ProductName, i => i.MapFrom(i => i.Product.ProductName));
            #endregion

            #region Warranty
            CreateMap<UpdateWarrantyRequest, Warranty>().ReverseMap();

            CreateMap<GetWarrantyResponse, Warranty>().ReverseMap();

            CreateMap<PaginatedList<Warranty>, PaginatedList<GetWarrantyResponse>>();

            CreateMap<CreateWarrantyRequest, Warranty>().ReverseMap();
            #endregion

            #region Category
            CreateMap<AddCategories, Category>().ReverseMap();

            CreateMap<Category, GetCategoryResponse>()
                .ForMember(c => c.Products, c => c.MapFrom(c => c.Products));

            CreateMap<Category, GetRawCategoryResponse>();
            #endregion

            #region GemPriceList
            CreateMap<CreateGemPriceRequest, GemPriceList>().ReverseMap();
            CreateMap<CreateGemPriceRequest, GemPriceList>().
                ForMember(p => p.EffDate, p => p.MapFrom(p => DateTime.Now));
            #endregion
        }
    }
}
