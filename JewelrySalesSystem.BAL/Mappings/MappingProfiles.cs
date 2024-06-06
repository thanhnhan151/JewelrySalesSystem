using AutoMapper;
using JewelrySalesSystem.BAL.Models.Roles;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            /*Change here*/
            CreateMap<RoleViewModel, Role>().ReverseMap();


        }
    }
}
