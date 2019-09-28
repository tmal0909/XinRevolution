using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace XinRevolution.Repository.Interface
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        TContext Context { get; set; }

        Hashtable Repositories { get; set; }

        IGenericRepository<TEntity> GetRepository<TEntity>();

        int Commit();
    }
}
