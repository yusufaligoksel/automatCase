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
        IEnumerable<TEntity> GetList();
        #endregion

        #region DataActionMethod
        TEntity Insert(TEntity entity);
        IEnumerable<TEntity> Insert(List<TEntity> entities);
        void Update(TEntity entity);
        void Update(List<TEntity> entities);
        void Delete(TEntity entity);
        void Delete(List<TEntity> entities);
        void Delete(object id);
        #endregion
    }
}
