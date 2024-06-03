using AutoMapper;
using JewelrySalesSystem.BAL.Models.Users;
using JewelrySalesSystem.DAL.Common;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, GetUserRequest>();
            CreateMap<PaginatedList<User>, PaginatedList<GetUserRequest>>();
            CreateMap<CreateUserRequest, User>();
            CreateMap<UpdateUserRequest, User>();
        }
    }
}
