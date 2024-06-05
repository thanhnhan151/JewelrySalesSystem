using AutoMapper;
using JewelrySalesSystem.BAL.Models.Gems;
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
            #endregion

            #region Material
            CreateMap<Material, MaterialItem>();
            #endregion
        }
    }
}
