using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BookShelf.BL;
using BookShelf.DAL;
using BookShelf.Entities;
using BookShelf.IoC;
using MVVMCommon;
using Ninject;

namespace BookShelf_Client.ViewModel
{
    public class MyBooksViewModel : ViewModelBase
    {
        private ReadersContext _readersContext;
        private IRepository<Book> _bookRepository; 
        private IRepository<User> _userRepository;
        private string _user;
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
        public string User
        {
            get { return _user; }
            set
            {
                _user = value;
                NotifyPropertyChanged("User");
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
        public MyBooksViewModel()
        {
            _user = App.CurrentUser;
            GetBooks();
        }

        private void GetBooks()
        {
            _bookRepository = IoCManager.Kernel.Get<IRepository<Book>>();
            var favoriteBooks = _bookRepository.GetByQery(q => q.IsFavorite);
            Books = new ObservableCollection<Book>(favoriteBooks);
        }

        private void CloseApplication()
        {
            Application.Current.MainWindow.Close();
        }

        private void DeleteBookFromFavorite()
        {
            
            _selectedBook.IsFavorite = false;
            _bookRepository.Update(_selectedBook);
            _bookRepository.SaveChanges();
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
        #region Navigation Commands & Methods
        private void GoToReadBooks(Frame BookShelfPageFrame)
        {
            BookShelfPageFrame.Navigate(new Uri("ReadBooks.xaml", UriKind.Relative));
        }
        private void GoToCategories(Frame BookShelfPageFrame)
        {
            BookShelfPageFrame.Navigate(new Uri("Categoties.xaml", UriKind.Relative));
        }
        private void GoToAllBooks(Frame BookShelfPageFrame)
        {
            BookShelfPageFrame.Navigate(new Uri("AllBooksStandartView.xaml", UriKind.Relative));
        }
        private void GoToMyBooksBook(Frame BookShelfPageFrame)
        {
            BookShelfPageFrame.Navigate(new Uri("MyBooks.xaml", UriKind.Relative));
        }

        private void GoToHelp(Frame BookShelfPageFrame)
        {
            Process.Start("https://helpx.adobe.com/ua/reader.html");
        }
        private void GoToReadingList()
        {
            ReadingList f2 = new ReadingList();
            f2.Show();
        }
        private void GoToUserSettings()
        {
            UserSetting settingsForm = new UserSetting();
            settingsForm.Show();
        }
        public ICommand GoToReadBooksCommand
        {

            get { return new RelayCommand<Frame>(GoToReadBooks); }
        }

        public ICommand DeleteBookFromFavoriteCommand
        {
            get { return  new RelayCommand(d => DeleteBookFromFavorite());}
        }

        public ICommand GoToAllBoooksCommand
        {
            get { return new RelayCommand<Frame>(GoToAllBooks); }
        }
        public ICommand GoToCategoriesCommand
        {
            get { return new RelayCommand<Frame>(GoToCategories); }
        }

        public ICommand GoToMyBooksCommand
        {
            get { return new RelayCommand<Frame>(GoToMyBooksBook); }

        }

        public ICommand GoToHelpCommand
        {
            get { return new RelayCommand<Frame>(GoToHelp);}
        }

        public ICommand GoToUserSettingsCommand
        {
            get { return new RelayCommand(g => GoToUserSettings()); }
        }
        public ICommand CloseApplicationCommand
        {
            get { return new RelayCommand(c => CloseApplication());}
        }
        public ICommand GoToReadingListCommand
        {
            get { return new RelayCommand(g => GoToReadingList());}
        }
        #endregion
    }


}
