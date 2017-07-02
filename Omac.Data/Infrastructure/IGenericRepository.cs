using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Omack.Data.Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        TEntity GetById(int id);
        TEntity GetById(int id, Expression<Func<TEntity, bool>> filter = null)
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int Id);
    }
}
