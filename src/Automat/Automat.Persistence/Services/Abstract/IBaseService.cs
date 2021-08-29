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
        Task<TEntity> FindAsync(object id);
        Task<IEnumerable<TEntity>> GetListAsync();
        #endregion

        #region DataActionMethod
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        Task<int> DeleteAsync(object id);
        #endregion
    }
}
