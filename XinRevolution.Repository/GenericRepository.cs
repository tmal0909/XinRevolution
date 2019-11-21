using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public DbContext Context { get; set; }

        public GenericRepository(DbContext context)
        {
            Context = context;
        }

        public TEntity Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);

            return entity;
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            var existEntity = Context.Entry(entity);

            existEntity.State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void Delete(object key)
        {
            var existEntity = Context.Set<TEntity>().Find(key);

            if (existEntity != default(TEntity))
                Context.Set<TEntity>().Remove(existEntity);
        }

        public void Delete(Expression<Func<TEntity, bool>> condition)
        {
            var existEntitys = Context.Set<TEntity>().Where(condition);

            if (existEntitys.Count() > 0)
                Context.Set<TEntity>().RemoveRange(existEntitys);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().AsNoTracking();
        }

        public IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return include(Context.Set<TEntity>().AsNoTracking()); 
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> condition)
        {
            return Context.Set<TEntity>().AsNoTracking().Where(condition);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> condition, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return include(Context.Set<TEntity>().AsNoTracking()).Where(condition);
        }

        public TEntity Single(object key)
        {
            return Context.Set<TEntity>().Find(key);
        }

        public TEntity Single(object key, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            var entities = include(Context.Set<TEntity>());
            var result = ((DbSet<TEntity>)entities).Find(key);

            return result;
        }

        public TEntity Single(Expression<Func<TEntity, bool>> condition)
        {
            return Context.Set<TEntity>().AsNoTracking().SingleOrDefault(condition);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> condition, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            return include(Context.Set<TEntity>().AsNoTracking()).SingleOrDefault(condition);
        }
    }
}
