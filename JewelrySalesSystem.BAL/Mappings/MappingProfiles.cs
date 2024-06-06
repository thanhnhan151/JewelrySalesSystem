using AutoMapper;
using JewelrySalesSystem.BAL.Models.Categories;
using JewelrySalesSystem.BAL.Models.Colours;
using JewelrySalesSystem.BAL.Models.Customers;
using JewelrySalesSystem.BAL.Models.GemPriceList;
using JewelrySalesSystem.BAL.Models.Gems;
using JewelrySalesSystem.BAL.Models.Genders;
using JewelrySalesSystem.BAL.Models.InvoiceDetails;
using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.BAL.Models.MaterialPriceList;
using JewelrySalesSystem.BAL.Models.Materials;
using JewelrySalesSystem.BAL.Models.Product;
using JewelrySalesSystem.BAL.Models.ProductGems;
using JewelrySalesSystem.BAL.Models.ProductMaterial;
using JewelrySalesSystem.BAL.Models.ProductTypes;
using JewelrySalesSystem.BAL.Models.Roles;
using JewelrySalesSystem.BAL.Models.Users;
using JewelrySalesSystem.BAL.Models.Warranties;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Colour, ColourViewModel>();
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<GemPriceList, GemPriceListViewModel>();
            CreateMap<Gem, GemViewModel>();
            CreateMap<Gender, GenderViewModel>();
            CreateMap<InvoiceDetail, InvoiceDetailViewModel>();
            CreateMap<Invoice, InvoiceViewModel>();
            CreateMap<MaterialPriceList, MaterialPriceListViewModel>();
            CreateMap<Material, MaterialViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductGem, ProductGemViewModel>();
            CreateMap<ProductMaterial, ProductMaterialViewModel>();
            CreateMap<Role, RoleViewModel>();
            CreateMap<ProductType, ProductTypeViewModel>();
            CreateMap<User, UserViewModel>();
            CreateMap<Warranty, WarrantyViewModel>();

        }
    }
}
