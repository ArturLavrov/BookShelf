using System;
using System.Data.Entity;
using BookShelf.DAL;
using BookShelf.Entities;
using BookShelf.IoC;
using Ninject;

namespace BookShelf.TestDB
{
    static class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<BookShelfContext>());
            
           
            
            var kernel = new StandardKernel(new NinjectConfiguration());
            var categoryrep = kernel.Get<IRepository<User>>();

            var user = new User()
            {
                Name = "Artur",
                Email = "arturstylus@gmail.com",
                Password = "qwerty",

            };
            categoryrep.Add(user);
            categoryrep.SaveChanges();

             Console.WriteLine("add user successfuly");
            Console.ReadKey();
        } 
    }
}
