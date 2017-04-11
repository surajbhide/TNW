using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using TNW.Models;

namespace TNW.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        T Get(int id);
        T Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeExpressions);
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions);
        void Remove(T entity);
        void Update(T entity);
    }
}