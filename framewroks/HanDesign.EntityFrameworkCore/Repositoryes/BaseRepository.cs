using HanDesign.Domain;
using HanDesign.Domain.Repositoryes;
using Microsoft.EntityFrameworkCore;

namespace HanDesign.EntityFrameworkCore.Repositoryes
{
    public class BasicRepository<TDbContext, TEntity, TKey>(TDbContext dbContext) : ReadOnlyBasicRepository<TDbContext, TEntity, TKey>(dbContext), IBaseRepository<TEntity, TKey> where TEntity : class, IEntity<TKey> where TDbContext : DbContext
    {
        public async Task<int> CreateAsync(TEntity entity)
        {
            await dbContext.Set<TEntity>().AddAsync(entity);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(List<TEntity> entities)
        {
            await dbContext.Set<TEntity>().AddRangeAsync(entities);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(List<TEntity> entities)
        {
            dbContext.Set<TEntity>().RemoveRange(entities);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
            return await dbContext.SaveChangesAsync();
        }
    }
}
