using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IRoleService
    {
        Task<Role> AddRoleAsync(Role role);
    }
}
