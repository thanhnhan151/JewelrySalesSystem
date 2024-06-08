namespace JewelrySalesSystem.DAL.Infrastructures
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);
        TEntity AddEntity(TEntity entity);
        void UpdateEntity(TEntity entity);

        //void DeleteEntity(TEntity entity);
    }
}
