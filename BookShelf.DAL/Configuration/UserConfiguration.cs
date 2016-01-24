using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelf.Entities;

namespace BookShelf.DAL
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(n => n.Name).IsRequired();
            Property(n => n.Password).IsRequired();
        }
    }
}
