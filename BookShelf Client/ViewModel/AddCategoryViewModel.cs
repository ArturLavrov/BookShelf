using System.Windows.Input;
using BookShelf.DAL;
using BookShelf.Entities;
using BookShelf.IoC;
using MVVMCommon;
using Ninject;

namespace BookShelf_Client.ViewModel
{
    public class AddCategoryViewModel:ViewModelBase
    {
        private IRepository<Category> _categoryRepository;
        private string _categorynamecontent;

        public string CategoryName
        {
            get { return _categorynamecontent;}
            set
            {
                _categorynamecontent = value;
                NotifyPropertyChanged("CategoryName");
            }
        }
        public AddCategoryViewModel()
        {
            GetRepository();
        }

        private void GetRepository()
        {
            _categoryRepository = IoCManager.Kernel.Get<IRepository<Category>>();
        }

        private void AddCategory()
        {
            var newcategory = new Category()
            {
                Name = _categorynamecontent,
            };
            _categoryRepository.Add(newcategory);
            _categoryRepository.SaveChanges();
        }

        public ICommand AddCategoryCommand
        {
            get { return new RelayCommand(a => AddCategory());}
        }
    }
}
