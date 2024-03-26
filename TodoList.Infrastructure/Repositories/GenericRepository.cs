using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Repositories;

namespace TodoList.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal TodoDbContext _context;
        internal DbSet<TEntity> _table;

        public GenericRepository(TodoDbContext context)
        {
            _context = context;
            _table = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetQueryable(
       Expression<Func<TEntity, bool>> filter = null,
       string includeProperties = "")
        {
            IQueryable<TEntity> query = _table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public IQueryable<TEntity> GetQueryableWithInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {

            IQueryable<TEntity> query = _table.Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public virtual TEntity GetById(object id)
        {
            return _table.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _table.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _table.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _table.Attach(entityToDelete);
            }
            _table.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _table.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
