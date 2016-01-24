using System.Diagnostics;
using System.IO;
using GhostscriptSharp;

namespace BookShelf.BL
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
    }
}
