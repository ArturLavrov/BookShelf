using System.Windows.Forms;

namespace BookShelf.BL.Services
{
    public class BookServices
    {
        private void MoveBookToBookShelfFolder(string sourseFile)
        {
            string destinationFolder = @"D:\BookShelfStorage";
            try
            {
                System.IO.File.Move(sourseFile, destinationFolder);
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
