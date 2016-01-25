using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using BookShelf.BL;
using BookShelf.BL.Services;
using BookShelf.DAL;
using BookShelf.Entities;
using BookShelf.IoC;
using Ninject;

namespace BookShelf_Client
{
    /// <summary>
    /// Interaction logic for Categoties.xaml
    /// </summary>
    public partial class Categoties : Page
    {
        private IRepository<Category> _categoriesRepository;
        public Categoties()
        {
            InitializeComponent();
        }

        private void DropFileInCategory(object sender, DragEventArgs e)
        {
            ImageService imgService = new ImageService();
            string[] fileList = e.Data.GetData(DataFormats.FileDrop) as string[];
            string fileextension = Path.GetExtension(fileList[0]);
            if (fileextension != ".pdf")
            {
                MessageBox.Show("Not allowed Drag'n'Drop for this file type");
            }
            else
            {
                string filename = Path.GetFileNameWithoutExtension(fileList[0]);
                string pathtofile = Path.GetFullPath(fileList[0]);
               
                string pathtobookimage = imgService.GenerateImage(pathtofile);
                FileInfo fileToAdd = new FileInfo(pathtofile);
                int size = (int)fileToAdd.Length;

                var book = new Book()
                {
                    Name = filename,
                    FileType = fileextension,
                    Path = pathtofile,
                    PathToBookImg = pathtobookimage,
                    Author = "Unknown Author",
                    Size = size
                };
                Debug.WriteLine(book.Name);
                _categoriesRepository = IoCManager.Kernel.Get<IRepository<Category>>();
                var concreteCategory = _categoriesRepository.GetByQery(q => q.Name == "Foreign Literature");
                concreteCategory.FirstOrDefault().Books.Add(book);
                _categoriesRepository.SaveChanges();
                MessageBox.Show("Book was added");
            }
        }

       
    }
}
