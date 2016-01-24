using System.Collections.ObjectModel;
using BookShelf.BL;
using BookShelf.DAL;
using BookShelf.Entities;
using BookShelf.IoC;
using MVVMCommon;
using Ninject;

namespace BookShelf_Client.ViewModel
{
    public class ReadBooksViewModel:ViewModelBase
    {
        private ReadersContext _readersContext;
        private IRepository<Book> _bookRepository;
        private Book _selectedBook;
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();


        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set
            {
                _books = value;
                NotifyPropertyChanged("Books");
            }
        }
        public Book SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;
                NotifyPropertyChanged("SelectedBook");
                ReadBook();
            }
        }
        public ReadBooksViewModel()
        {
            GetBooks();
        }

        private void GetBooks()
        {
            _bookRepository = IoCManager.Kernel.Get<IRepository<Book>>();
            var readBooks = _bookRepository.GetByQery(q => q.IsRead);
            Books = new ObservableCollection<Book>(readBooks);
        }

        private void ReadBook()
        {
            switch (_selectedBook.FileType)
            {
                case ".pdf":
                    _readersContext = new ReadersContext(new PDFReader());
                    _readersContext.Read(_selectedBook.Path);
                    break;
                case ".djvu":
                    _readersContext = new ReadersContext(new DJVuReader());
                    _readersContext.Read(_selectedBook.Path);
                    break;
            }
        }



    }
}
