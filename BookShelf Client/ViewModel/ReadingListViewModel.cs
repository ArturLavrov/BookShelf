using System.Windows;
using BookShelf.DAL;
using BookShelf.Entities;
using MVVMCommon;
using ICommand = System.Windows.Input.ICommand;
namespace BookShelf_Client.ViewModel
{
    public class ReadingListViewModel:ViewModelBase
    {
        private string _readinglistContent;
        private IRepository<User> _userRepository;
        private User _user;
        private FontWeight _fontWeight;
       

        private FontWeight ChangeFontWeigh()
        {
            _fontWeight =  FontWeights.Bold;
            return _fontWeight;
        }
      
        public string ReadingListContent
        {
            get { return _readinglistContent; }
            set
            {
                _readinglistContent = value;
                NotifyPropertyChanged("ReadingListContent");
            }
        }

        
        public ReadingListViewModel()
        {
            LoadReadingList();
        }

        private void LoadReadingList()
        {
            //_userRepository = IoCManager.Kernel.Get<IRepository<User>>();
            //_user = _userRepository.GetById(1);
            //ReadingListContent = _user.ReadingList.RadingList;
        }

        private void SaveReadingList()
        {
            _user.ReadingList.RadingList = ReadingListContent;
            _userRepository.Update(_user);
            _userRepository.SaveChanges();
        }

        private void ShereInTwitter()
        {
            
        }

        private void CloseWindow()
        {
            //Shit)
            var w = Application.Current.Windows[2];
            w.Hide();
        }


        public ICommand ShereInTwitterCommand
        {
            get { return new RelayCommand(s => ShereInTwitter());}
        }

        public ICommand SaveReadingListCommand
        {
            get { return new RelayCommand(s => SaveReadingList()); }
        }

        public ICommand CloseWindowCommand
        {
            get { return new RelayCommand(c => CloseWindow());}
        }

        public ICommand ChangeFontWeightCommand
        {
            get { return new RelayCommand(c => ChangeFontWeigh()); }
        }
    }
}
