using AutoMapper;
using JewelrySalesSystem.BAL.Models.Categories;
using JewelrySalesSystem.BAL.Models.Colours;
using JewelrySalesSystem.BAL.Models.Gems;
using JewelrySalesSystem.BAL.Models.Genders;
using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.BAL.Models.Materials;
using JewelrySalesSystem.BAL.Models.Products;
using JewelrySalesSystem.BAL.Models.ProductTypes;
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
            CreateMap<CreateRoleRequest, Role>();
            #endregion

            #region Gender
            CreateMap<Gender, GetGenderResponse>();

            CreateMap<CreateGenderRequest, Gender>();
            #endregion

            #region User
            CreateMap<User, GetUserResponse>()
                .ForMember(u => u.Role, u => u.MapFrom(u => u.Role.RoleName));

            CreateMap<PaginatedList<User>, PaginatedList<GetUserResponse>>();

            CreateMap<CreateUserRequest, User>();

            CreateMap<UpdateUserRequest, User>();
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

            CreateMap<Product, ProductAndProductTypeResponse>()
                .ForMember(p => p.Category, p => p.MapFrom(p => p.Category.CategoryName))
                .ForMember(p => p.Gender, p => p.MapFrom(p => p.Gender.GenderName))
                .ForMember(p => p.Colour, p => p.MapFrom(p => p.Colour.ColourName));
            #endregion

            #region Gem
            CreateMap<Gem, GemItem>()
                .ForMember(g => g.GemPrice, g => g.MapFrom(m => m.GemPrice));

            CreateMap<Gem, GetGemResponse>()
                .ForMember(g => g.GemPrice, g => g.MapFrom(m => m.GemPrice));

            CreateMap<PaginatedList<Gem>, PaginatedList<GetGemResponse>>();

            CreateMap<GemPriceList, GemPrice>();

            CreateMap<UpdateGemRequest, Gem>().ReverseMap();
            #endregion

            #region Material
            CreateMap<Material, MaterialItem>()
                .ForMember(m => m.MaterialPrice, m => m.MapFrom(m => m.MaterialPrices.SingleOrDefault()));

            CreateMap<Material, GetMaterialResponse>()
                .ForMember(m => m.MaterialPrice, m => m.MapFrom(m => m.MaterialPrices.SingleOrDefault()));

            CreateMap<PaginatedList<Material>, PaginatedList<GetMaterialResponse>>();

            CreateMap<MaterialPriceList, MaterialPrice>();
            #endregion

            #region Invoice
            CreateMap<Invoice, GetInvoiceResponse>()
                .ForMember(i => i.Items, i => i.MapFrom(i => i.InvoiceDetails))

                //.ForMember(i => i.Total, i => i.MapFrom(i => i.InvoiceDetails
                //.Sum(i => i.ProductPrice)))

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
            CreateMap<CreateCategoryRequest, Category>();

            CreateMap<UpdateCategoryRequest, Category>();

            CreateMap<Category, GetCategoryResponse>()
                .ForMember(c => c.Products, c => c.MapFrom(c => c.Products));

            CreateMap<Category, GetRawCategoryResponse>();
            #endregion

            #region ProductType
            CreateMap<ProductType, GetProductTypeResponse>();

            CreateMap<ProductType, ProductTypeIdCollectionResponse>()
                .ForMember(pt => pt.Products, pt => pt.MapFrom(pt => pt.Products));

            CreateMap<ProductType, CreateProductTypeRequest>().ReverseMap();
            #endregion

            #region Colour
            CreateMap<Colour, GetColourResponse>();
            #endregion
        }
    }
}
