using JewelrySalesSystem.BAL.Models.Roles;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IRoleService
    {
        Task<RoleViewModel> AddRoleAsync(RoleViewModel role);
    }
}
