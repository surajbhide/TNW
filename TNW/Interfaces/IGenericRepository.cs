using System.Collections.Generic;
using TNW.Models;

namespace TNW.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Remove(T entity);
        void Update(T entity);
    }
}