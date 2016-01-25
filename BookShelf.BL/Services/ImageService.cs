using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using GhostscriptSharp;

namespace BookShelf.BL.Services
{
    public class ImageService
    {
        public string  GenerateImage(string pathToFile)
        {
            string filename = Path.GetFileNameWithoutExtension(pathToFile);
            string path = "D:\\BookImages\\" + filename + ".png";
            Debug.WriteLine(path);
            GhostscriptWrapper.GeneratePageThumb(pathToFile,
              path, 1, 120, 250);
            return path;
        }

        public void DeleteBookPicture(string picturepath)
        {
            try
            {
                File.Delete(picturepath);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
