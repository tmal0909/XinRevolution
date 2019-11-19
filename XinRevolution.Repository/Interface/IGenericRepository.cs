﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XinRevolution.Repository.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        DbContext Context { get; set; }

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(string include);

        IEnumerable<TEntity> GetAll<TInclude>(Expression<Func<TEntity, TInclude>> include);
        
        IEnumerable<TEntity> GetAll(IEnumerable<string> includes);

        IEnumerable<TEntity> GetAll<TInclude>(IEnumerable<Expression<Func<TEntity, TInclude>>> includes);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> condition);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> condition, string include);

        IEnumerable<TEntity> GetAll<TInclude>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TInclude>> include);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> condition, IEnumerable<string> includes);

        IEnumerable<TEntity> GetAll<TInclude>(Expression<Func<TEntity, bool>> condition, IEnumerable<Expression<Func<TEntity, TInclude>>> includes);

        TEntity Single(object key);

        TEntity Single(object key, string include);

        TEntity Single<TInclude>(object key, Expression<Func<TEntity, TInclude>> include);

        TEntity Single(object key, IEnumerable<string> includes);

        TEntity Single<TInclude>(object key, IEnumerable<Expression<Func<TEntity, TInclude>>> includes);

        TEntity Single(Expression<Func<TEntity, bool>> condition);

        TEntity Single(Expression<Func<TEntity, bool>> condition, string include);

        TEntity Single<TInclude>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TInclude>> include);

        TEntity Single(Expression<Func<TEntity, bool>> condition, IEnumerable<string> includes);

        TEntity Single<TInclude>(Expression<Func<TEntity, bool>> condition, IEnumerable<Expression<Func<TEntity, TInclude>>> includes);

        TEntity Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(object key);

        void Delete(Expression<Func<TEntity, bool>> condition);
    }
}
