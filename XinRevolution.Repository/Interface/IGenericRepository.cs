using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace XinRevolution.Repository.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        DbContext Context { get; set; }

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> condition);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> condition, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);

        TEntity Single(object key);

        TEntity Single(object key, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);

        TEntity Single(Expression<Func<TEntity, bool>> condition);

        TEntity Single(Expression<Func<TEntity, bool>> condition, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);

        TEntity Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(object key);

        void Delete(Expression<Func<TEntity, bool>> condition);
    }
}
