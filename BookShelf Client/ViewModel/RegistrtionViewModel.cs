using System.Windows;
using MVVMCommon;
using ICommand = System.Windows.Input.ICommand;
using BookShelf.DAL;
using BookShelf.Entities;
using BookShelf.IoC;
using Ninject;

namespace BookShelf_Client.ViewModel
{
    public class RegistrtionViewModel:ViewModelBase
    {
        public static bool _oAuthenbled;
        private string _userName;
        private string _userPassword;
        private string _userEmail;
        private IRepository<User> _userRepository;


        public string UserName
        {
            get { return _userName;}
            set
            {
                _userName = value;
                NotifyPropertyChanged("UserName");
            }
        }

        public string UserPassword
        {
            get { return _userPassword;}
            set
            {
                _userPassword = value;
                NotifyPropertyChanged("UserPassword");
            }
        }

        public string UserEmail
        {
            get { return _userEmail;}
            set
            {
                _userEmail = value;
                NotifyPropertyChanged("UserEmail");
            }
        }
        public RegistrtionViewModel()
        {
            _userRepository = IoCManager.Kernel.Get<IRepository<User>>();
        }
        
        private void SignIn()
        {
            User bookShelfUser = new User()
            {
                Email = UserEmail,
                Name = UserName,
                Password = UserPassword
            };
            _userRepository.Add(bookShelfUser);
            _userRepository.SaveChanges();

            MyBoooks myBoooks = new MyBoooks();
            myBoooks.Show();
            var window = Application.Current.Windows[0];
            window.Hide();
            
        }

        private void TwitterSignIn()
        {
            _oAuthenbled = true;
            SignIn();
        }

        public ICommand SignInCommand
        {
            get { return new RelayCommand(s => SignIn());}
        }

        public ICommand TwitterSignInCommand
        {
            get { return new RelayCommand(ts => TwitterSignIn());}
        }
    }
}
