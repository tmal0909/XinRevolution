using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XinRevolution.Repository.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        DbContext Context { get; set; }

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, object>> include);

        IEnumerable<TEntity> GetAll(IEnumerable<Expression<Func<TEntity, object>>> includes);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> condition);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> include);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> condition, IEnumerable<Expression<Func<TEntity, object>>> includes);

        TEntity Single(object key);

        TEntity Single(object key, Expression<Func<TEntity, object>> include);

        TEntity Single(object key, IEnumerable<Expression<Func<TEntity, object>>> includes);

        TEntity Single(Expression<Func<TEntity, bool>> condition);

        TEntity Single(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> include);

        TEntity Single(Expression<Func<TEntity, bool>> condition, IEnumerable<Expression<Func<TEntity, object>>> includes);

        TEntity Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(object key);

        void Delete(Expression<Func<TEntity, bool>> condition);
    }
}
