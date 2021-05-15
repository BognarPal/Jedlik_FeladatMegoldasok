using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace pizzeria.data.interfaces
{
    public interface IGenericRepository<T> where T: class
    {
        List<T> GetAll();
        T GetById(int id);
        List<T> Search(Expression<Func<T, bool>> predicate);
        
        T Add(T entity);        
        T AddRange(IEnumerable<T> entities);
        
        T Update(T entity);
        
        T Remove(T entity);
        T RemoveRange(T entities);
    }
}
