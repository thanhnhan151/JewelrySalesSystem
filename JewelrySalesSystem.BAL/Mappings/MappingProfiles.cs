using AutoMapper;
using JewelrySalesSystem.BAL.Models.Gender;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {

            CreateMap<GenderModel, Gender>().ReverseMap();
        
        }


    }
}
