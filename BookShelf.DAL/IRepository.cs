using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BookShelf.DAL
{
   public interface IRepository<T>
   {
       IEnumerable<T> GetAll();
       IEnumerable<T> GetByQery(Expression<Func<T, bool>> predicate);
       T GetById(int id);
       void Add(T entity);
       void Update(T entity);
       void Delete(T entity);
       void SaveChanges();
   }
}
