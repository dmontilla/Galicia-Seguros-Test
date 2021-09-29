using Galicia.Test.Core.Base;
using Galicia.Test.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Galicia.Test.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly TestDbContext context;
        private readonly DbSet<TEntity> dbSet;
        public BaseRepository(TestDbContext context)
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

        public void Update(TEntity entity)
        {
            context.Attach(entity).State = EntityState.Modified;
            dbSet.Update(entity);
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }

    }

}
