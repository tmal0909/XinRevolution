using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Repository
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        public TContext Context { get; set; }

        public Hashtable Repositories { get; set; }

        public UnitOfWork(DbContext context)
        {
            Context = (TContext)context;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (Repositories == null)
                Repositories = new Hashtable();

            var key = typeof(TEntity).Name;

            if (Repositories.ContainsKey(key))
                return (IGenericRepository<TEntity>)Repositories[key];

            var repository = new GenericRepository<TEntity>(this.Context);

            Repositories.Add(key, repository);

            return repository;
        }

        public void RollBack()
        {
            var changedEntries = Context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged);

            foreach(var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        public int Commit()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(true);
        }
    }
}
