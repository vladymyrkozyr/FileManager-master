using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FileManager.DAL.Contexts;

namespace FileManager.DAL.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal FileManagerDbContext Context;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(FileManagerDbContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return DbSet.SqlQuery(query, parameters).ToList();
        }

        public virtual IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = DbSet;

            return query.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);

        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
