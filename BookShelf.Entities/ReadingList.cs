using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShelf.Entities
{
    public class ReadingList
    {
        public ReadingList()
        {
            
        }
        [Key, ForeignKey("User")]
        public int UserId { get; set; }
        public string RadingList { get; set; }

        public virtual User User { get; set; }
    }
}
