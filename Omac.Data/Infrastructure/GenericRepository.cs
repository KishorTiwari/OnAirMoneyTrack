using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Omack.Data.DAL;

namespace Omack.Data.Infrastructure
{
    public abstract class GenericRepository<TEntity> where TEntity: class
    {
        private OmackContext context;
        private DbSet<TEntity> dbSet;

        public GenericRepository(OmackContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null )
        {
            IQueryable<TEntity> query = dbSet; 
            if(filter != null)
            {
                 query = query.Where(filter);
            }
            return query;
        }
        public TEntity GetSingle(Expression<Func<TEntity, bool>> filter = null)
        {
            return dbSet.SingleOrDefault(filter);
        }
        public TEntity GetById (int id)
        {
            return dbSet.Find(id);  
        }         
        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
            //return entity;          
        }
        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(int Id)
        {
            var entity = dbSet.Find(Id);
            if(entity != null)
            {
                dbSet.Remove(entity);
            }
        }
    } 
}
