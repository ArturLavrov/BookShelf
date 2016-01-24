using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using BookShelf.Entities;

namespace BookShelf.DAL
{
    public interface IBookShelfContext
    {
        IDbSet<Book> Books { get; set; }
        IDbSet<Category> Categories { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<ReadingList> ReadingList { get; set; }
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry Entry(object entity);
        void SaveChanges();
    }
}
