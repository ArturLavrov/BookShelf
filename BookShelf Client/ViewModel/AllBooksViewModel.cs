﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using BookShelf.BL;
using BookShelf.BL.Services;
using BookShelf.DAL;
using BookShelf.Entities;
using BookShelf.IoC;
using MVVMCommon;
using Ninject;

namespace BookShelf_Client.ViewModel
{
    public class AllBooksViewModel:ViewModelBase
    {
        private ReadersContext _readersContext;
        private IRepository<Book> _bookRepository;
        private IRepository<Category> _categoriesRepository; 
        private Book _selectedBook;
        private Category _selectedCategory;
        private const string Searchfilter = "PDF files (*.pdf*)|*.pdf|DjVu files (*.djvu)|*.djvu";
        private string _pathToFile;
        private string _searchfield;
        private string _pathtobookimage;
        private ObservableCollection<Book> _books = new ObservableCollection<Book>();
        private ObservableCollection<Category> _categories = new ObservableCollection<Category>(); 
        public ObservableCollection<Book> Books
        {
            get { return _books;}
            set
            {
                _books = value;
                NotifyPropertyChanged("Books");
            }
        }

        public ObservableCollection<Category> Categories
        {
            get { return _categories;}
            set
            {
                _categories = value;
                NotifyPropertyChanged("Categories");
            }
        } 
        public string SearchField
        {
            get { return _searchfield; }
            set
            {
                _searchfield = value;
                NotifyPropertyChanged("SearchField");
                SearchInBooks();
            }
        }

        public Book SelectedBook
        {
            get { return _selectedBook;}
            set
            {
                _selectedBook = value;
                NotifyPropertyChanged("SelectedBook");
            }
        }

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                AddBookToSelectedCategory();
            }
        }
        public AllBooksViewModel()
        {
            GetAllUserBooks();
            GetAllCategories();
        }

        private void GetAllUserBooks()
        {
            _bookRepository = IoCManager.Kernel.Get<IRepository<Book>>();
            var allbooks = _bookRepository.GetAll();
            Books = new ObservableCollection<Book>(allbooks);
        }

        private void GetAllCategories()
        {
            _categoriesRepository = IoCManager.Kernel.Get<IRepository<Category>>();
            var allcategories = _categoriesRepository.GetAll();
            Categories = new ObservableCollection<Category>(allcategories);
        }
        private void AddBook()
        {
            BookServices bookServices = new BookServices();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = Searchfilter
            };
            openFileDialog.ShowDialog();

            _pathToFile = bookServices.MoveBookToBookShelfFolder(Path.GetFullPath(openFileDialog.FileName));

            FileInfo fileToAdd = new FileInfo(_pathToFile);
            int size = (int)fileToAdd.Length;

            string fileType = Path.GetExtension(openFileDialog.FileName);
            if (fileType == ".pdf")
            {
                //TODO Convert this in separete method to avoid code duplication
                //Implementation of template Method
                ImageService imageService = new ImageService();
                _pathtobookimage = imageService.GenerateImage(_pathToFile);
            }

            var book = new Book()
            {
                Name = Path.GetFileNameWithoutExtension(openFileDialog.FileName),
                FileType = fileType,
                Path = _pathToFile,
                PathToBookImg = _pathtobookimage,
                Author = "Unknown Author",
                Size = size
            };

            _bookRepository.Add(book);
            _bookRepository.SaveChanges();

            var allbooks = _bookRepository.GetAll();
            Books = new ObservableCollection<Book>(allbooks);
        }

        private void AddBookToSelectedCategory()
        {
            var repository  = _categoriesRepository.GetById(_selectedCategory.CategoryId);
            repository.Books.Add(_selectedBook);
            _categoriesRepository.SaveChanges();
        }
        private void DeleteBook()
        {
            _bookRepository.Delete(_selectedBook);
            _bookRepository.SaveChanges();
            DeleteBookCover();
            var allbooks = _bookRepository.GetAll();
            Books = new ObservableCollection<Book>(allbooks);
        }

        private void DeleteBookCover()
        {
            ImageService service = new ImageService();
            service.DeleteBookPicture(_selectedBook.PathToBookImg);
        }
        private void SearchInBooks()
        {  
            string mask = SearchField.ToLower();
            var bookfilterlist = _books.Where(b => b.Name.Contains(mask) || b.Author.Contains(mask)).ToList();
            Books = new ObservableCollection<Book>(bookfilterlist);
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

        private void AddToFavorive()
        {
            _selectedBook.IsFavorite = true;
            _bookRepository.Update(_selectedBook);
            _bookRepository.SaveChanges();
        }

        private void AddToReadBooks()
        {
            _selectedBook.IsRead = true;
            _bookRepository.Update(_selectedBook);
            _bookRepository.SaveChanges();
        }
        public ICommand AddBookCommand
        {
            get { return new RelayCommand(a =>  AddBook());}
        }

        public ICommand ReadBookCommand
        {
            get { return new RelayCommand(r => ReadBook());}
        }

        public ICommand DeleteBookCommand
        {
            get { return new RelayCommand(d => DeleteBook());}
        }

        public ICommand AddToFavoriteCommand
        {
            get { return new RelayCommand(a => AddToFavorive());}
        }

        public ICommand AddToReadBooksCommand
        {
            get { return new RelayCommand(a => AddToReadBooks());}
        }
    }
}
