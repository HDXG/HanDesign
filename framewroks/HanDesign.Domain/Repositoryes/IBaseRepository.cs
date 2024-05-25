namespace HanDesign.Domain.Repositoryes
{
    public interface IBaseRepository<TEntity, in TKey> : IReadOnlyBasicRepository<TEntity, TKey> where TEntity : class,IEntity<TKey>
    {
        Task<int> CreateAsync(TEntity entity);
        Task<int> CreateAsync(List<TEntity> entities);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        Task<int> DeleteAsync(List<TEntity> entities);
    }
}
