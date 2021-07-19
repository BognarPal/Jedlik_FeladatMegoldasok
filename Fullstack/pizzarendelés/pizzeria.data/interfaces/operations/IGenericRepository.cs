using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace pizzeria.data.interfaces.operations
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> Search(Expression<Func<T, bool>> predicate);

        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);

        T Update(T entity);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        void Save();
    }
}
