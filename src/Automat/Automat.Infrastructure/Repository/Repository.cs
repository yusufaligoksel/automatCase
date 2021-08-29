using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Automat.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Automat.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields
        private readonly AutomatContext _context;
        private DbSet<TEntity> _entities;
        #endregion

        #region Constructor
        public Repository(AutomatContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }
        protected virtual DbSet<TEntity> Entities => _entities ?? (_entities = _context.Set<TEntity>());
        #endregion

        #region Get
        public IQueryable<TEntity> Table => Entities;

        public IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public async Task<IEnumerable<TEntity>> GetListAsync()
        {
            return await _entities.ToListAsync();
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _entities.FindAsync(keyValues);
        }
        #endregion

        #region DataActionMedhods
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _entities.AddAsync(entity);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> DeleteAsync(object id)
        {
            var entity = await FindAsync(id);
            _context.Remove(entity);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
             var result= await _context.SaveChangesAsync();
             return result;
        }
        #endregion

    }
}
