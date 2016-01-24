using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BookShelf.BL;
using BookShelf.DAL;
using BookShelf.Entities;
using BookShelf.IoC;
using MVVMCommon;
using Ninject;
using MessageBox = System.Windows.MessageBox;

namespace BookShelf_Client.ViewModel
{
    public class CategoryViewModel:ViewModelBase
    {
        private ReadersContext _readersContext;
        private IRepository<Category> _categoriesRepository;
        private IRepository<Book> _bookRepository; 
        private ObservableCollection<Category> _categories = new ObservableCollection<Category>();
        private ObservableCollection<Book> _booksInCategory = new ObservableCollection<Book>();
        private Category _selectedCategory;
        private Book _selectedBookInCategory;
        private const string _searchfilter = "Text files (*.txt)|*.txt|PDF files (*.pdf*)|*.pdf|DjVu files (*.djvu)|*.djvu";
        private string _pathToFile;
        public ObservableCollection<Book> Books
        {
            get { return _booksInCategory;}
            set
            {
                _booksInCategory = value;
                NotifyPropertyChanged("Books");
            }
        } 

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                NotifyPropertyChanged("Categories");
            }
        }

        public Category SelectedCategory
        {
            get { return _selectedCategory;}
            set
            {
                _selectedCategory = value;
                NotifyPropertyChanged("SelectedCategory");
                GetBooksByCategory();
            }
        }

        public Book SelectedBookInCategory
        {
            get { return _selectedBookInCategory;}
            set
            {
                _selectedBookInCategory = value;
                NotifyPropertyChanged("SelectedBookInCategory");
                ReadBook();
            }
        }
        public CategoryViewModel()
        {
            GetAllCategories();
        }
        private void GetAllCategories()
        {
            _categoriesRepository = IoCManager.Kernel.Get<IRepository<Category>>();
            var allcategories = _categoriesRepository.GetAll();
            Categories = new ObservableCollection<Category>(allcategories);
        }

        private void GetBooksByCategory()
        {
            if (_selectedCategory == null) return;
            var concreteCategory = _categoriesRepository.GetByQery(q => q.Name == _selectedCategory.Name);
            Books = new ObservableCollection<Book>(concreteCategory.FirstOrDefault().Books);
        }

        private void DeleteCategory()
        {
            MessageBoxResult dialogResult =  MessageBox.Show("Are you sure you want to delete category", "Delate Category", MessageBoxButton.YesNoCancel);
            if (dialogResult != MessageBoxResult.Yes)
            {
                return;
            }
            _categoriesRepository.Delete(_selectedCategory);
            _categoriesRepository.SaveChanges();
            var allcategories = _categoriesRepository.GetAll();
            Categories = new ObservableCollection<Category>(allcategories);
        }

        private void AddCategory()
        {
            AddCategory f2 = new AddCategory();
            f2.Show();
        }
        private void ReadBook()
        {
            if (_selectedBookInCategory == null) return;
            switch (_selectedBookInCategory.FileType)
            {
                case ".pdf":
                    _readersContext = new ReadersContext(new PDFReader());
                    _readersContext.Read(SelectedBookInCategory.Path);
                    break;
                case ".djvu":
                    _readersContext = new ReadersContext(new DJVuReader());
                    _readersContext.Read(_selectedBookInCategory.Path);
                    break;
            }
        }

        public ICommand AddCategoryCommand
        {
            get { return new RelayCommand(a => AddCategory());}
        }

        public ICommand DeleteCategoryCommand
        {
            get { return new RelayCommand(d => DeleteCategory());}
        }
    }
}
