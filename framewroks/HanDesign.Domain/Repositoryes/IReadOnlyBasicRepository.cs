using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HanDesign.Domain.Repositoryes
{
    public interface IReadOnlyBasicRepository<TEntity,in TKey> where TEntity : class,IEntity<TKey>
    {
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> wherePredicate);
        Task<List<TEntityEntity>> GetListAsync<TEntityEntity>(string sql, params object[] parameters);
        Task<List<TEntityEntity>> GetListAsync<TEntityEntity>(FormattableString sql);
        Task<(int, List<TEntity>)> GetPageListAsync(Expression<Func<TEntity, bool>> wherePredicate, Func<TEntity, TKey> orderPredicate, int pageIndex=1,int pageSize=10 );
        Task<TEntity?> GetAsync(Expression<Func<TEntity,bool>> wherePredicate);
        Task<TEntity?> GetAsync(FormattableString sql);
        Task<TEntityEntity?> GetAsync<TEntityEntity>(string sql, params object[] parameters);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> wherePredicate);
        Task<int> GetCountAsync();
        Task<int> ExecuteSqlInterpolatedAsync(FormattableString sql);
        Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters);
    }
}
