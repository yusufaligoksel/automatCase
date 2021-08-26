using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Automat.Common.Extensions;
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

        public TEntity Find(params object[] keyValues)
        {
            return _entities.Find(keyValues);
        }

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

        public IEnumerable<TEntity> GetList()
        {
            return Entities.ToList();
        }
        #endregion

        #region DataActionMedhods

        public void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Add(entity);
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.AddAsync(entity);
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChangesAsync();
        }

        public void InsertRange(List<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var item in entities)
            {
                Entities.Add(item);
                _context.Entry(item).State = EntityState.Added;
            }

            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateAsync(TEntity entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }

        public void UpdateRange(List<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var item in entities)
            {
                _entities.Attach(item);
                _context.Entry(item).State = EntityState.Modified;
            }

            _context.SaveChanges();
        }

        public void Delete(object id)
        {
            var entity = Find(id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteAsync(object id)
        {
            var entity = FindAsync(id);
            _context.Remove(entity);
            _context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
            _context.SaveChangesAsync();
        }

        public void DeleteRange(List<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            foreach (var item in entities)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
        }

        #endregion

        #region SqlQuery
        public IEnumerable<TEntity> GetSql(string sql, params object[] parameters)
        {
            return Entities.FromSqlRaw(sql, parameters);
        }
        #endregion

        #region Include
        public IQueryable<TEntity> IncludeMany(params Expression<Func<TEntity, object>>[] includes)
        {
            return _entities.IncludeMultiple(includes);
        }
        #endregion
    }
}
