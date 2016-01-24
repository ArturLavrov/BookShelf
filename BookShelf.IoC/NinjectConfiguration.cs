using BookShelf.DAL;
using BookShelf.Entities;
using Ninject.Modules;

namespace BookShelf.IoC
{
    public class NinjectConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Book>>().To<Repository<Book>>();
            Bind<IRepository<User>>().To<Repository<User>>();
            Bind<IRepository<Category>>().To<Repository<Category>>();
            Bind<IRepository<ReadingList>>().To<Repository<ReadingList>>().InSingletonScope();
           
            Bind<IBookShelfContext>().To<BookShelfContext>().InSingletonScope();

        }
    }
}
