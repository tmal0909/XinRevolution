using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().AsNoTracking();
        }

        public IEnumerable<TEntity> GetAll(string include)
        {
            return Context.Set<TEntity>().AsNoTracking().Include(include);
        }

        public IEnumerable<TEntity> GetAll(IEnumerable<string> includes)
        {
            var result = Context.Set<TEntity>().AsNoTracking().AsQueryable();

            foreach (var include in includes)
            {
                result = result.Include(include);
            }

            return result;
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> condition)
        {
            return Context.Set<TEntity>().AsNoTracking().Where(condition);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> condition, string include)
        {
            return Context.Set<TEntity>().AsNoTracking().Include(include).Where(condition);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> condition, IEnumerable<string> includes)
        {
            var result = Context.Set<TEntity>().AsNoTracking().Where(condition).AsQueryable();

            foreach (var include in includes)
            {
                result = result.Include(include);
            }

            return result;
        }

        public TEntity Single(object key)
        {
            var entity = Context.Set<TEntity>().Find(key);

            Context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public TEntity Single(object key, string include)
        {
            var result = Context.Set<TEntity>().AsNoTracking().Include(include);

            return ((DbSet<TEntity>)result).Find(key);
        }

        public TEntity Single(object key, IEnumerable<string> includes)
        {
            var result = Context.Set<TEntity>().AsNoTracking().AsQueryable();

            foreach (var include in includes)
            {
                result = result.Include(include);
            }

            return ((DbSet<TEntity>)result).Find(key);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> condition)
        {
            return Context.Set<TEntity>().AsNoTracking().SingleOrDefault(condition);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> condition, string include)
        {
            return Context.Set<TEntity>().AsNoTracking().Include(include).SingleOrDefault(condition);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> condition, IEnumerable<string> includes)
        {
            var result = Context.Set<TEntity>().AsNoTracking().AsQueryable();

            foreach (var include in includes)
            {
                result = result.Include(include);
            }

            return result.SingleOrDefault(condition);
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
    }
}
