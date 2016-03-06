using System.IO;
using System.Windows.Forms;

namespace BookShelf.BL.Services
{
    public class BookServices
    {
        public string  MoveBookToBookShelfFolder(string sourseFile)
        {
            string filename = Path.GetFileName(sourseFile);
            string destinationFile = @"D:\BookShelfStorage" + @"\" + filename;
           
            try
            {
                File.Move(sourseFile, destinationFile);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            return destinationFile;
        }
    }
}
