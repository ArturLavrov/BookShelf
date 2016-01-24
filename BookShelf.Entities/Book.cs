namespace BookShelf.Entities
{
    public class Book
    {
        public Book()
        {
            
        }

        public int BookId { get; set; }
        public string Name { get; set; }
        public bool IsRead { get; set; }
        public bool IsFavorite { get; set; }
        public string PathToBookImg { get; set; }
        public string Author { get; set; }
        public string Ganre { get; set; }
        public int Size { get; set; }
        public string FileType { get; set; }
        public string Path { get; set; }
        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
    }
}
