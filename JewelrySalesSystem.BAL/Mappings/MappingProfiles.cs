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
            CreateMap<Product, GetProductResponse>();
            CreateMap<PaginatedList<Product>, PaginatedList<GetProductResponse>>();
            #endregion

            #region Gem
            CreateMap<List<Gem>, List<GemItem>>();
            #endregion

            #region Material
            CreateMap<List<Material>, List<MaterialItem>>();
            #endregion
        }
    }
}
