using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetQueryable(
       Expression<Func<T, bool>> filter = null,
       string includeProperties = "");
        IQueryable<T> GetQueryableWithInclude(
        Expression<Func<T, bool>> predicate, 
        params Expression<Func<T, object>>[] includes);
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
    }
}
