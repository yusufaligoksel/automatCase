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

        public async Task<TEntity> FindAsync(object id)
        {
            return await _repository.FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetListAsync()
        {
            return await _repository.GetListAsync();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var result = await _repository.InsertAsync(entity);
            return result;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = await _repository.UpdateAsync(entity);
            return result;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            var result=await _repository.DeleteAsync(entity);
            return result;
        }

        public async Task<int> DeleteAsync(object id)
        {
            var result= await _repository.DeleteAsync(id);
            return result;
        }
    }
}
