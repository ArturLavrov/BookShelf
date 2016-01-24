using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using BookShelf.Entities;

namespace BookShelf.DAL
{
    public class BookShelfContext : DbContext, IBookShelfContext
    {
        public IDbSet<Book> Books { get; set; }
        public IDbSet<User> Users { get; set; }

        public IDbSet<ReadingList> ReadingList { get; set; }
        public IDbSet<Category> Categories { get; set; }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public DbEntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        public void SaveChanges()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
