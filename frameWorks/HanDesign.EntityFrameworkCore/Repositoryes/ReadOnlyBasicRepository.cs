using System.Linq.Expressions;
using HanDesign.Domain;
using HanDesign.Domain.Repositoryes;
using Microsoft.EntityFrameworkCore;

namespace HanDesign.EntityFrameworkCore.Repositoryes
{
    public class ReadOnlyBasicRepository<TDbContext, TEntity, TKey>(TDbContext dbContext) : IReadOnlyBasicRepository<TEntity, TKey> where TEntity : class, IEntity<TKey> where TDbContext : DbContext
    {
        protected virtual DbSet<TEntity> GetDbSet() => dbContext.Set<TEntity>();
        protected virtual IQueryable<TEntity> GetQueryable() => GetDbSet().AsNoTracking();
        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> wherePredicate) => GetQueryable().Where(wherePredicate).ToListAsync();

        public Task<List<TEntityEntity>> GetListAsync<TEntityEntity>(string sql, params object[] parameters)
        {
            return dbContext.Database.SqlQueryRaw<TEntityEntity>(sql, parameters).ToListAsync();
        }

        public Task<List<TEntityEntity>> GetListAsync<TEntityEntity>(FormattableString sql)
        {
            return dbContext.Database.SqlQuery<TEntityEntity>(sql).ToListAsync();
        }
        public async Task<(int, List<TEntity>)> GetPageListAsync(Expression<Func<TEntity, bool>> wherePredicate, Func<TEntity, TKey> orderPredicate, int pageIndex = 1, int pageSize = 10)
        {
            var query = GetQueryable().Where(wherePredicate);
            int count=await query.CountAsync();
            var list = query.OrderByDescending(orderPredicate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return (count,list);
        }
        public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> wherePredicate) => GetQueryable().Where(wherePredicate).FirstOrDefaultAsync();

        public Task<TEntity?> GetAsync(FormattableString sql) => GetDbSet().FromSql(sql).FirstOrDefaultAsync();

        public Task<TEntityEntity?> GetAsync<TEntityEntity>(string sql, params object[] parameters)
        {
            return dbContext.Database.SqlQueryRaw<TEntityEntity>(sql, parameters).FirstOrDefaultAsync();
        }
        public Task<int> GetCountAsync(Expression<Func<TEntity, bool>> wherePredicate) => GetQueryable().Where(wherePredicate).CountAsync();

        public Task<int> GetCountAsync() => GetQueryable().CountAsync();
        public Task<int> ExecuteSqlInterpolatedAsync(FormattableString sql)
        {
            return dbContext.Database.ExecuteSqlInterpolatedAsync(sql);
        }

        public Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters)
        {
            return dbContext.Database.ExecuteSqlRawAsync(sql, parameters);
        }
    }
}
