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
        TEntity Find(params object[] keyValues);
        IEnumerable<TEntity> GetList();
        Task<IEnumerable<TEntity>> GetListAsync();
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindAsync(params object[] keyValues);
        #endregion

        #region DataAction
        void Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        void InsertRange(List<TEntity> entities);

        void Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        void UpdateRange(List<TEntity> entities);

        void Delete(object id);
        Task<int> DeleteAsync(object id);
        void Delete(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        void DeleteRange(List<TEntity> entities);
        #endregion

        #region SqlQuery
        IEnumerable<TEntity> GetSql(string sql, params object[] parameters);
        #endregion

        #region Include
        IQueryable<TEntity> IncludeMany(params Expression<Func<TEntity, object>>[] includes);
        #endregion

    }
}
