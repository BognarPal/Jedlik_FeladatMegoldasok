using pizzeria.data.interfaces.models;
using pizzeria.data.interfaces.operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace pizzeria.service.repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        protected readonly ApplicationDbContext dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual T Add(T entity)
        {
            var entityEntry = dbContext.Set<T>().Add(entity);
            return entityEntry.Entity;
        }

        public virtual IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            dbContext.Set<T>().AddRange(entities);
            return entities.ToList();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }

        public virtual T GetById(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return dbContext.Set<T>().Find(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public virtual void Remove(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            dbContext.Set<T>().RemoveRange(entities);
        }

        public virtual IEnumerable<T> Search(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Where(predicate).ToList();
        }

        public virtual T Update(T entity)
        {
            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            return entity;
        }

        public virtual void Save()
        {
            dbContext.SaveChanges();
        }
    }
}