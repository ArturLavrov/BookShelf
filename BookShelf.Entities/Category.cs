using System.Collections.Generic;

namespace BookShelf.Entities
{
    public class Category
    {
        public Category()
        {
           Books = new List<Book>();   
        }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
