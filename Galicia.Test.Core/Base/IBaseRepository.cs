using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Galicia.Test.Core.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> FindByIdAsync(object id);
        Task<TEntity> FindByIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> GetAllIncludeAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> WhereAsync(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteById(object id);
        void SaveChanges();
    }
}
