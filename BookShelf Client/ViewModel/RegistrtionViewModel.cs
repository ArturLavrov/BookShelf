using System.Windows;
using MVVMCommon;
using ICommand = System.Windows.Input.ICommand;

namespace BookShelf_Client.ViewModel
{
    public class RegistrtionViewModel:ViewModelBase
    {
        public static bool _oAuthenbled;

        public RegistrtionViewModel()
        {
            
        }
        
        private void SignIn()
        {
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
