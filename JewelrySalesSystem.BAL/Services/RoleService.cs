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

        public async Task<CreateRoleRequest> AddRoleAsync(CreateRoleRequest createRoleRequest)
        {
            var result = _unitOfWork.Roles.AddEntity(_mapper.Map<Role>(createRoleRequest));

            await _unitOfWork.CompleteAsync();

            return createRoleRequest;
        }
    }
}
