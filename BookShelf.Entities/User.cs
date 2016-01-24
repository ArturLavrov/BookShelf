using System.Collections.Generic;

namespace BookShelf.Entities
{
    public class User
    {
        public User()
        {
            Books = new List<Book>();
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ReadingList ReadingList { get; set; }

        
    }
}
