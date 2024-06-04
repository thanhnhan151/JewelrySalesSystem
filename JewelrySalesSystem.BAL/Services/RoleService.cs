using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.DAL.Entities;
using JewelrySalesSystem.DAL.Infrastructures;

namespace JewelrySalesSystem.BAL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public async Task<Role> AddRoleAsync(Role role)
        {
            var result = _unitOfWork.Roles.AddEntity(role);
            await _unitOfWork.CompleteAsync();
            return result;
        }
    }
}
