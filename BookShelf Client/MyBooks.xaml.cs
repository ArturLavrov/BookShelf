using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookShelf_Client
{
    /// <summary>
    /// Interaction logic for My_Books.xaml
    /// </summary>
    public partial class My_Books : Page
    {
        public My_Books()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReadingList f2 = new ReadingList();
            f2.Show();
        }
    }
}
