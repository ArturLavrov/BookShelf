using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelf.Entities;

namespace BookShelf.DAL
{
    public class BookConfiguration : EntityTypeConfiguration<Book>
    {
        public BookConfiguration()
        {
            Property(n => n.Name).IsRequired();
            Property(n => n.Author).IsRequired();
            Property(n => n.FileType).IsRequired();
            Property(n => n.Path).IsRequired();
        }
    }
}
