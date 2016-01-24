using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BookShelf.DAL
{
    public class Repository<T> : IRepository <T> where T : class 
    {
        private readonly IDbSet<T> dbSet;

        private readonly IBookShelfContext context;

        public Repository(IBookShelfContext datacontext)
        {
            context = datacontext;
            dbSet = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public IEnumerable<T> GetByQery(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
