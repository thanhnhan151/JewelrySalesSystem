namespace JewelrySalesSystem.BAL.Interfaces
{
    public interface IUserService
    {
        Task<bool> LoginAsync(string email, string passWord);
    }
}
