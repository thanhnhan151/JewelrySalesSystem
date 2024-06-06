using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Roles;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RoleViewModel> AddRoleAsync(RoleViewModel role)
        {
            var result = _unitOfWork.Roles.AddEntity(_mapper.Map<Role>(role));
            await _unitOfWork.CompleteAsync();
            var newRole = _mapper.Map<RoleViewModel>(result);
            return newRole;
        }
    }
}
