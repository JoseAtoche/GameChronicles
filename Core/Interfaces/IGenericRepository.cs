namespace Domain.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
