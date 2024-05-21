using JewelrySalesSystem.DAL.Interfaces;

namespace JewelrySalesSystem.DAL.Infrastructures
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task CompleteAsync();
    }
}
