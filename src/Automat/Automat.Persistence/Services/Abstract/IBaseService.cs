using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Persistence.Services.Abstract
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        #region GetData
        TEntity Find(object id);
        Task<TEntity> FindAsync(object id);
        IEnumerable<TEntity> GetList();
        Task<IEnumerable<TEntity>> GetListAsync();
        #endregion

        #region DataActionMethod
        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        IEnumerable<TEntity> Insert(List<TEntity> entities);
        void Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        void Update(List<TEntity> entities);
        void Delete(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        void Delete(List<TEntity> entities);
        void Delete(object id);
        Task<int> DeleteAsync(object id);
        #endregion
    }
}
