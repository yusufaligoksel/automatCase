using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat.Infrastructure.Repository;
using Automat.Persistence.Services.Abstract;

namespace Automat.Persistence.Services.Concrete
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        public BaseService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public TEntity Find(object id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<TEntity> GetList()
        {
            return _repository.GetList();
        }

        public TEntity Insert(TEntity entity)
        {
            _repository.Insert(entity);
            return entity;
        }

        public IEnumerable<TEntity> Insert(List<TEntity> entities)
        {
            _repository.InsertRange(entities);
            return entities;
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
        }

        public void Update(List<TEntity> entities)
        {
            _repository.UpdateRange(entities);
        }

        public void Delete(TEntity entity)
        {
            _repository.Delete(entity);
        }

        public void Delete(List<TEntity> entities)
        {
            _repository.DeleteRange(entities);
        }

        public void Delete(object id)
        {
            _repository.Delete(id);
        }
    }
}
