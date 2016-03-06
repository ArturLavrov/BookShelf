using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Navigation;
using BookShelf.DAL;
using BookShelf.Entities;
using BookShelf.IoC;
using Ninject;
using User = BookShelf.Entities.User;

namespace BookShelf_Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IRepository<User> _userRepository;
        public static string  CurrentUser;
        void App_Startup(object sender, StartupEventArgs e)
        {
            IoCManager.Start();
          
            _userRepository = IoCManager.Kernel.Get<IRepository<User>>();
            //TO DO think how to improve quality of this solution.
            //Perhaps create some buffer 
            //like LocalStorage or Cookies for registration purpose.
            var currentUser = _userRepository.GetById(1);
            if (currentUser != null)
            {
                CurrentUser = currentUser.Name;
                MyBoooks myBoooks = new MyBoooks();
                myBoooks.Show();
            }
            else
            {
                Registration window = new Registration();
                window.Show();
            }
        }
    }
}

