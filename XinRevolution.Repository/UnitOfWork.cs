using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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
