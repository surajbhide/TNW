using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TNW.Interfaces;

namespace TNW.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationDbContext _db;

        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public T Get(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public T Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> q = _db.Set<T>();
            foreach (var include in includeExpressions)
            {
                q = q.Include(include);
            }
            return q.FirstOrDefault(where);
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> q = _db.Set<T>();
            foreach (var include in includeExpressions)
            {
                q = q.Include(include);
            }
            return q.AsEnumerable();
        }

        public void Remove(T entity)
        {
            _db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(T entity)
        {
            _db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
