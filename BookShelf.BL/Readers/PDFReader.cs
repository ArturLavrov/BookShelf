using System.Diagnostics;

namespace BookShelf.BL
{
    public class PDFReader : IAbstractReader
    {
        public void Read(string filepath)
        {
            Process.Start(filepath);
        }
    }
}
