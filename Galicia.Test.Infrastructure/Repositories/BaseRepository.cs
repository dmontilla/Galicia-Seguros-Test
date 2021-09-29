using Galicia.Test.Core.Base;
using Galicia.Test.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Galicia.Test.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly TestContext context;
        private readonly DbSet<TEntity> dbSet;
        public BaseRepository(TestContext context)
        {
            this.context = context;
            dbSet = this.context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
            context.Entry(entity).State = EntityState.Added;
        }

        public void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            context.Remove(entity);
        }

        public void DeleteById(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public async Task<TEntity> FindByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }
        private IQueryable<TEntity> GetAllInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = dbSet.AsNoTracking();
            queryable = includeProperties
                     .Aggregate(queryable, (current, property) => current.Include(property));
            return queryable;
        }

        public async Task<TEntity> FindByIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAllInclude(includeProperties);
            TEntity results = await query.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
            return results;
        }
        public async Task<IEnumerable<TEntity>> GetAllIncludeAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAllInclude(includeProperties);
            return await query.ToListAsync();
        }
        public async Task<TEntity> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            context.Attach(entity).State = EntityState.Modified;
            dbSet.Update(entity);
        }

    }

}
