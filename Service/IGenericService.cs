namespace Domain.Interfaces
{
    public interface IGenericService<TEntity>
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
