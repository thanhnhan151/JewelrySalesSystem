namespace JewelrySalesSystem.DAL.Infrastructures
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<bool> AddEntityAsync(TEntity entity);
        Task<bool> UpdateEntityAsync(TEntity entity);
    }
}
