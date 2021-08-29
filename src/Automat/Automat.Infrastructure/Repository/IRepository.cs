using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Get
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }
        Task<IEnumerable<TEntity>> GetListAsync();
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindAsync(params object[] keyValues);
        #endregion

        #region DataAction
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(object id);
        Task<int> DeleteAsync(TEntity entity);
        #endregion
    }
}
